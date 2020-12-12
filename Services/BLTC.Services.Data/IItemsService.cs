namespace BLTC.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BLTC.Data.Models;

    public interface IItemsService
    {
        Task<int> Add(string name, int type, int shape, decimal weight, decimal purity, int fineness, int quantity, string dimensions, string description, int manufacturer);

        Task AddImagesToItem(List<Image> images, int itemId);

        Task Edit(Item item);

        Task Delete(int itemId);

        Task<Item> GetItemById(int itemId);

        Task<int> GetIdByName(string name);

        IEnumerable<T> GetItem<T>(int itemId);

        IEnumerable<T> GetAllItems<T>();

        IEnumerable<T> GetAllPendingItems<T>();

        IEnumerable<T> GetAllApprovedItems<T>();

        IEnumerable<KeyValuePair<string, string>> GetKeyValuesOfEnum(Type type);
    }
}
