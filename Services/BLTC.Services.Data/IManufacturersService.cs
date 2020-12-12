namespace BLTC.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BLTC.Data.Models;

    public interface IManufacturersService
    {
        Task<int> GetIdByName(string name);

        Task<Manufacturer> GetById(int id);

        IEnumerable<T> GetManufacturer<T>(int id);

        IEnumerable<Item> GetAllProducts(int id);

        IEnumerable<Item> GetAllApprovedProducts(int id);

        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
    }
}
