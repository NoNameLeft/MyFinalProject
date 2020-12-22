namespace BLTC.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BLTC.Data.Models;
    using BLTC.Services.Data.Models;
    using Microsoft.AspNetCore.Http;

    public interface IItemsService
    {
        Task CreateAsync(CreateItemDto dto, string userId, IFormFileCollection images, string imagePath);

        Task UpdateAsync(UpdateItemDto dto, string userId, string imagePath);

        Task DeleteAsync(int id);

        Task ChangeStatus(int id);

        void AddImagesToItem(List<Image> images, int id);

        T GetItemById<T>(int id);

        IEnumerable<T> GetAllItems<T>();

        IEnumerable<T> GetAllPendingItems<T>();

        IEnumerable<T> GetAllApprovedItems<T>();

        IEnumerable<T> GetItemsByFilter<T>(string type);

        IEnumerable<T> GetItemsByFilter<T>(string shape, string type);

        IEnumerable<T> GetItemsByFilter<T>(string type, string shape, decimal? weight);

        IEnumerable<KeyValuePair<string, string>> GetKeyValuesOfEnum(Type type);
    }
}
