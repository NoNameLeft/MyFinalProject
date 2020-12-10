namespace BLTC.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BLTC.Data.Models;

    public interface IItemsService
    {
        Task<int> Add(string name, int type, int shape, decimal weight, decimal purity, int fineness, int quantity, string dimensions, string description, int manufacturer);

        Task<Item> GetItemById(int itemId);

        Task<int> GetIdByName(string name);

        IEnumerable<T> GetItem<T>(int itemId);

        public IEnumerable<T> GetAllItems<T>();

        void AddImagesToItem(List<Image> images, int itemId);

        IEnumerable<KeyValuePair<string, string>> GetKeyValuesOfEnum(Type type);
    }
}
