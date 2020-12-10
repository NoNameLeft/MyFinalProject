namespace BLTC.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BLTC.Data.Common.Repositories;
    using BLTC.Data.Models;

    public class ImagesService : IImagesService
    {
        private readonly IDeletableEntityRepository<Image> imagesRepository;

        public ImagesService(IDeletableEntityRepository<Image> imagesRepository)
        {
            this.imagesRepository = imagesRepository;
        }

        public async Task<List<Image>> Add(string addedById, IReadOnlyDictionary<string, string> imagesNameAndExtention, Type type, int typeId)
        {
            foreach (var currentImage in imagesNameAndExtention)
            {
                var image = new Image
                {
                    AddedByEmployeeId = addedById,
                    Name = currentImage.Key,
                    Extension = this.ChangeContentType(currentImage.Value),
                    CreatedOn = DateTime.UtcNow,
                };

                this.GetPropertyAndAssingValue(type, image, typeId);

                await this.imagesRepository.AddAsync(image);
                await this.imagesRepository.SaveChangesAsync();
            }

            return this.imagesRepository.AllAsNoTracking().Where(x => x.ItemId == typeId).ToList();
        }

        public async Task<Image> GetImageById(string imageId)
        {
            return await Task.FromResult(this.imagesRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == imageId));
        }

        private void GetPropertyAndAssingValue(Type type, Image image, int typeId)
        {
            // validate if such property exist
            var typeProperty = typeof(Image).GetProperties().Where(x => x.Name == type.Name + "Id").FirstOrDefault();
            typeof(Image).GetProperty(typeProperty.Name).SetValue(image, typeId);
        }

        private string ChangeContentType(string extention)
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
