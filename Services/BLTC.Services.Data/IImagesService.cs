namespace BLTC.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BLTC.Data.Models;

    public interface IImagesService
    {
        public Task<List<Image>> Add(string addedById, IReadOnlyDictionary<string, string> imagesNameAndExtention, Type type, int typeId);

        Task<Image> GetImageById(string imageId);
    }
}
