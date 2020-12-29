namespace BLTC.Services.Data
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using BLTC.Data.Common.Repositories;
    using BLTC.Data.Models;
    using BLTC.Services.Data.Models;
    using BLTC.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class ManufacturersService : IManufacturersService
    {
        private readonly IDeletableEntityRepository<Manufacturer> manufacturersRepository;
        private readonly IDeletableEntityRepository<Country> countriesRepository;

        public ManufacturersService(
            IDeletableEntityRepository<Manufacturer> manufacturersRepository,
            IDeletableEntityRepository<Country> countriesRepository)
        {
            this.manufacturersRepository = manufacturersRepository;
            this.countriesRepository = countriesRepository;
        }

        public T GetById<T>(int id)
        {
            return this.manufacturersRepository.AllAsNoTracking().Where(x => x.Id == id).To<T>().SingleOrDefault();
        }

        public T GetByName<T>(string name)
        {
            return this.manufacturersRepository.AllAsNoTracking().Where(x => x.Name == name).To<T>().SingleOrDefault();
        }

        public IEnumerable<T> GetManufacturer<T>(int id)
        {
            return this.manufacturersRepository.AllAsNoTracking().Where(x => x.Id == id).To<T>();
        }

        public IEnumerable<Item> GetAllApprovedProducts(int productId)
        {
            return this.manufacturersRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == productId).Products.Where(x => x.IsApproved).AsEnumerable();
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.manufacturersRepository.AllAsNoTracking().Select(x => new
            {
                x.Name,
                x.Id,
            }).ToList().Select(x => new KeyValuePair<string, string>(x.Name, x.Id.ToString())).AsEnumerable();
        }

        public async Task AddAsync(CreateManufacturerDto inputDto, IFormFile image, string imageFolder)
        {
            if (!this.manufacturersRepository.All().Any(x => x.Name == inputDto.Name))
            {
                var manufacturer = new Manufacturer
                {
                    Name = inputDto.Name,
                    Overview = inputDto.Overview,
                    CountryId = this.countriesRepository.All().SingleOrDefault(x => x.IsoCode == inputDto.IsoCode).Id,
                    Logo = new Image
                    {
                        Name = image.FileName,
                        AddedByEmployeeId = inputDto.UserId,
                        Extension = this.ChangeToContentType(Path.GetExtension(image.FileName).Replace(".", string.Empty).Trim()),
                    },
                };

                await this.UploadImagesToDirectory(imageFolder, image);
                await this.manufacturersRepository.AddAsync(manufacturer);
                await this.manufacturersRepository.SaveChangesAsync();
            }
        }

        private async Task UploadImagesToDirectory(string imagesFolder, IFormFile image)
        {
            Directory.CreateDirectory($"{imagesFolder}");
            if (!this.ExistingFileNames(imagesFolder).Any(x => x == image.FileName))
            {
                var path = Path.Combine(imagesFolder, image.FileName);
                await image.CopyToAsync(new FileStream(path, FileMode.Create));
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
