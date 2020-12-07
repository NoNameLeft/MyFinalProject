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
                    Extension = currentImage.Value,
                    CreatedOn = DateTime.UtcNow,
                };

                this.GetPropertyAndAssingValue(type, image, typeId);

                await this.imagesRepository.AddAsync(image);
                await this.imagesRepository.SaveChangesAsync();
            }

            return this.imagesRepository.AllAsNoTracking().Where(x => x.ItemId == typeId).ToList();
        }

        public void GetAllByTypeAndId(object obj)
        {
            // make it works...
        }

        private T GetPropValue<T>(object src, string propName)
        {
            return (T)src.GetType().GetProperty(propName)?.GetValue(src, null);
        }

        private void GetPropertyAndAssingValue(Type type, Image image, int typeId)
        {
            // validate if such property exist
            var typeProperty = typeof(Image).GetProperties().Where(x => x.Name == type.Name + "Id").FirstOrDefault();
            typeof(Image).GetProperty(typeProperty.Name).SetValue(image, typeId);
        }
    }
}
