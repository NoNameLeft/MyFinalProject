namespace BLTC.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BLTC.Data.Models;
    using BLTC.Services.Data.Models;
    using Microsoft.AspNetCore.Http;

    public interface IManufacturersService
    {
        Task AddAsync(CreateManufacturerDto inputDto, IFormFile logo, string imageFolder);

        T GetByName<T>(string name);

        T GetById<T>(int id);

        IEnumerable<T> GetManufacturer<T>(int id);

        IEnumerable<Item> GetAllApprovedProducts(int id);

        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
    }
}
