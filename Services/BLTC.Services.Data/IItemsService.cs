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

        void AddImagesToItem(List<Image> images, int itemId);

        Task<IEnumerable<KeyValuePair<string, string>>> GetKeyValuesOfEnum(Type type);
    }
}
