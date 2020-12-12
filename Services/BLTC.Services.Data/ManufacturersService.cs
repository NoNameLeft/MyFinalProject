namespace BLTC.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BLTC.Data.Common.Repositories;
    using BLTC.Data.Models;
    using BLTC.Services.Mapping;

    public class ManufacturersService : IManufacturersService
    {
        private readonly IDeletableEntityRepository<Manufacturer> manufacturersRepository;

        public ManufacturersService(IDeletableEntityRepository<Manufacturer> manufacturersRepository)
        {
            this.manufacturersRepository = manufacturersRepository;
        }

        public async Task<int> GetIdByName(string name)
        {
            return await Task.FromResult(this.manufacturersRepository.AllAsNoTracking().FirstOrDefault(x => x.Name == name).Id);
        }

        public async Task<Manufacturer> GetById(int id)
        {
            return await Task.FromResult(this.manufacturersRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == id));
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.manufacturersRepository.AllAsNoTracking().Select(x => new
            {
                x.Name,
                x.Id,
            }).ToList().Select(x => new KeyValuePair<string, string>(x.Name, x.Id.ToString()));
        }

        public IEnumerable<T> GetManufacturer<T>(int id)
        {
            return this.manufacturersRepository.All().Where(x => x.Id == id).To<T>();
        }

        public IEnumerable<Item> GetAllProducts(int productId)
        {
            return this.manufacturersRepository.All().FirstOrDefault(x => x.Id == productId).Products.ToList();
        }

        public IEnumerable<Item> GetAllApprovedProducts(int productId)
        {
            return this.manufacturersRepository.All().FirstOrDefault(x => x.Id == productId).Products.Where(x => x.IsApproved).ToList();
        }
    }
}
