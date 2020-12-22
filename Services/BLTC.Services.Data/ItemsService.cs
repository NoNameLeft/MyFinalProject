namespace BLTC.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using BLTC.Data.Common.Repositories;
    using BLTC.Data.Models;
    using BLTC.Data.Models.Enums;
    using BLTC.Services.Data.Models;
    using BLTC.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class ItemsService : IItemsService
    {
        private readonly string pattern = @"^(.*)\.(jpe?g|png|svg)$";
        private readonly decimal biggestPossibleWeight = 12500.0m;

        private readonly IDeletableEntityRepository<Item> itemsRepository;
        private readonly IDeletableEntityRepository<Image> imagesRepository;

        public ItemsService(
            IDeletableEntityRepository<Item> itemsRepository,
            IDeletableEntityRepository<Image> imagesRepository)
        {
            this.itemsRepository = itemsRepository;
            this.imagesRepository = imagesRepository;
        }

        public async Task CreateAsync(CreateItemDto dto, string userId, IFormFileCollection images, string imagesFolder)
        {
            var item = this.itemsRepository.All().SingleOrDefault(x => x.Name == dto.Name);
            if (item != null)
            {
                throw new Exception($"Item {dto.Name} already exists.");
            }

            item = new Item()
            {
                Name = dto.Name,
                Type = (ItemType)Enum.ToObject(typeof(ItemType), dto.Type),
                Shape = (ItemShape)Enum.ToObject(typeof(ItemShape), dto.Shape),
                Weight = dto.Weight,
                Purity = dto.Purity,
                Fineness = (Carats)Enum.ToObject(typeof(Carats), dto.Fineness),
                Price = this.CalculateItemPrice(), // implement!!!
                Quantity = dto.Quantity,
                Dimensions = dto.Dimensions,
                Description = dto.Description,
                ManufacturerId = dto.ManufacturerId,
            };

            foreach (var inputImage in images)
            {
                var image = this.imagesRepository.All().SingleOrDefault(x => x.Name == inputImage.FileName);
                if (image != null)
                {
                    throw new Exception($"You cannot impload {inputImage} twice.");
                }
                else if (!Regex.IsMatch(inputImage.FileName, this.pattern))
                {
                    throw new Exception($"Invalid image extention {Path.GetExtension(inputImage.FileName)}");
                }

                image = new Image
                {
                    AddedByEmployeeId = userId,
                    Name = inputImage.FileName,
                    Extension = this.ChangeToContentType(Path.GetExtension(inputImage.FileName).Replace(".", string.Empty).Trim()),
                };

                item.Images.Add(image);
            }

            await this.UploadImagesToDirectory(imagesFolder, images);
            await this.itemsRepository.AddAsync(item);
            await this.itemsRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateItemDto dto, string userId, string imagesFolder)
        {
            var item = this.itemsRepository.All().SingleOrDefault(x => x.Id == dto.ItemId);
            var itemImages = this.imagesRepository.All().Where(x => x.ItemId == item.Id).ToList(); // teq kadeto sym gi vkaral pri Create

            await this.RemoveImagesAsync(dto, imagesFolder, itemImages);

            foreach (var image in dto.Images)
            {
                if (this.imagesRepository.All().Any(x => x.Name == image.FileName && x.ItemId != item.Id))
                {
                    throw new Exception($"This image {image.FileName} is already uploaded to {item.Name}.");
                }

                if (!itemImages.Any(x => x.Name == image.FileName))
                {
                    item.Images.Add(new Image() { Name = image.FileName, Extension = this.ChangeToContentType(Path.GetExtension(image.FileName).Replace(".", string.Empty).Trim()), AddedByEmployeeId = userId, ItemId = item.Id });
                }
            }

            item.Name = dto.Name;
            item.Type = (ItemType)dto.Type;
            item.Shape = (ItemShape)dto.Shape;
            item.Weight = dto.Weight;
            item.Purity = dto.Purity;
            item.Quantity = dto.Quantity;
            item.Dimensions = dto.Dimensions;
            item.Description = dto.Description;
            item.ManufacturerId = dto.ManufacturerId;

            await this.UploadImagesToDirectory(imagesFolder, dto.Images);
            await this.itemsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int itemId)
        {
            var item = this.itemsRepository.All().Where(x => x.Id == itemId).SingleOrDefault();
            this.itemsRepository.Delete(item);

            await this.itemsRepository.SaveChangesAsync();
        }

        public async Task ChangeStatus(int itemId)
        {
            var item = this.itemsRepository.All().SingleOrDefault(x => x.Id == itemId);
            if (item.IsApproved)
            {
                item.IsApproved = false;
            }
            else
            {
                item.IsApproved = true;
            }

            await this.itemsRepository.SaveChangesAsync();
        }

        public void AddImagesToItem(List<Image> images, int itemId)
        {
            var item = this.GetItemById<Item>(itemId);
            images.ForEach(i => item.Images.Add(i));
        }

        public T GetItemById<T>(int itemId)
        {
            return this.itemsRepository.AllAsNoTracking().Where(x => x.Id == itemId).To<T>().SingleOrDefault();
        }

        public IEnumerable<T> GetAllItems<T>()
        {
            return this.itemsRepository.AllAsNoTracking().To<T>().AsEnumerable();
        }

        public IEnumerable<T> GetItemsByFilter<T>(string type)
        {
            if (!Enum.TryParse<ItemType>(type, out ItemType itemType))
            {
                throw new Exception($"This {type} is invalid.");
            }

            return this.itemsRepository.AllAsNoTracking().Where(x => x.Type == itemType && x.IsApproved).To<T>().ToList();
        }

        public IEnumerable<T> GetItemsByFilter<T>(string type, string shape)
        {
            if (!Enum.TryParse<ItemType>(type, out ItemType itemType))
            {
                throw new Exception($"This {type} is invalid.");
            }

            if (!Enum.TryParse<ItemShape>(shape, out ItemShape itemShape))
            {
                throw new Exception($"This {shape} is invalid.");
            }

            return this.itemsRepository.AllAsNoTracking().Where(x => x.Type == itemType && x.Shape == itemShape && x.IsApproved).To<T>().ToList();
        }

        public IEnumerable<T> GetItemsByFilter<T>(string type, string shape, decimal? weight)
        {
            if (!Enum.TryParse<ItemType>(type, out ItemType itemType))
            {
                throw new Exception($"This {type} is invalid.");
            }

            if (!Enum.TryParse<ItemShape>(shape, out ItemShape itemShape))
            {
                throw new Exception($"This {shape} is invalid.");
            }

            if (weight <= 0 || weight > this.biggestPossibleWeight)
            {
                throw new Exception($"There is no item with that weight {weight} grams");
            }

            return this.itemsRepository.AllAsNoTracking().Where(x => x.Type == itemType && x.Shape == itemShape && x.Weight == weight && x.IsApproved).To<T>().ToList();
        }

        public IEnumerable<T> GetAllPendingItems<T>()
        {
            return this.itemsRepository.AllAsNoTracking().Where(x => !x.IsApproved).To<T>().AsEnumerable();
        }

        public IEnumerable<T> GetAllApprovedItems<T>()
        {
            return this.itemsRepository.AllAsNoTracking().Where(x => x.IsApproved).To<T>().AsEnumerable();
        }

        public IEnumerable<KeyValuePair<string, string>> GetKeyValuesOfEnum(Type enumType)
        {
            var displayNames = this.GetDisplayNames(enumType);
            var names = enumType.GetEnumNames();
            var values = enumType.GetEnumValues().Cast<int>().ToList();
            if (displayNames.Count > 0)
            {
                return names.Select((n, index) =>
                             new KeyValuePair<string, string>(displayNames[index], values[index].ToString())).AsEnumerable();
            }

            return names.Select((n, index) =>
                             new KeyValuePair<string, string>(n, values[index].ToString())).AsEnumerable();
        }

        private decimal CalculateItemPrice()
        {
            // Implement method which calls web api and calculate the current price of the metals.
            // Add disclamer based on what you calculate the price
            var dailyGoldPricePerGram = 50.0m;
            var currentPrice = dailyGoldPricePerGram + (dailyGoldPricePerGram * 0.33m);

            return currentPrice;
        }

        private List<string> GetDisplayNames(Type type)
        {
            var displaynames = new List<string>();
            var names = Enum.GetNames(type);
            foreach (var name in names)
            {
                var field = type.GetField(name);
                var fds = field.GetCustomAttributes(typeof(DisplayAttribute), true);

                if (fds.Length == 0)
                {
                    displaynames.Add(name);
                }

                foreach (DisplayAttribute fd in fds)
                {
                    displaynames.Add(fd.Name);
                }
            }

            return displaynames;
        }

        private async Task UploadImagesToDirectory(string imagesFolder, IFormFileCollection imageFiles)
        {
            Directory.CreateDirectory($"{imagesFolder}");
            foreach (var image in imageFiles)
            {
                if (!this.ExistingFileNames(imagesFolder).Any(x => x == image.FileName))
                {
                    var path = Path.Combine(imagesFolder, image.FileName);
                    await image.CopyToAsync(new FileStream(path, FileMode.Create));
                }
            }
        }

        private async Task RemoveImagesAsync(UpdateItemDto dto, string imagesFolder, List<Image> itemImages)
        {
            foreach (var img in itemImages)
            {
                var filePath = Path.Combine(imagesFolder, img.Name);
                var test = dto.Images.Where(x => x.FileName == img.Name).SingleOrDefault();
                if (File.Exists(filePath) && test is null)
                {
                    File.Delete(filePath);

                    this.imagesRepository.Delete(this.imagesRepository.All().SingleOrDefault(x => x.Name == img.Name));
                    await this.imagesRepository.SaveChangesAsync();
                }
            }
        }

        private string[] ExistingFileNames(string folder)
        {
            return Directory.GetFiles(folder)
                            .Select(Path.GetFileName)
                            .ToArray();
        }

        private string ChangeToContentType(string extention)
        {
            switch (extention)
            {
                case "jpg":
                    extention = "image/jpeg";
                    break;
                case "png":
                    extention = "image/png";
                    break;
                case "gif":
                    extention = "image/gif";
                    break;
                case "svg":
                    extention = "image/svg+xml";
                    break;
                case "tiff":
                    extention = "image/tiff";
                    break;
                case "webp":
                    extention = "image/webp";
                    break;
                case "bmp":
                    extention = "image/bmp";
                    break;
            }

            return extention;
        }
    }
}
