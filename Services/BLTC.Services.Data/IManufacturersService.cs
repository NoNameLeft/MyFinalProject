namespace BLTC.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BLTC.Data.Models;

    public interface IManufacturersService
    {
        T GetByName<T>(string name);

        T GetById<T>(int id);

        IEnumerable<T> GetManufacturer<T>(int id);

        IEnumerable<Item> GetAllApprovedProducts(int id);

        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
    }
}
