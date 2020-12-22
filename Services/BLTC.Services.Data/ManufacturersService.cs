namespace BLTC.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

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

        public T GetById<T>(int id)
        {
            return this.manufacturersRepository.AllAsNoTracking().Where(x => x.Id == id).To<T>().SingleOrDefault();
        }

        public T GetByName<T>(string name)
        {
            return this.manufacturersRepository.AllAsNoTracking().Where(x => x.Name == name).To<T>().SingleOrDefault();
        }

        public IEnumerable<T> GetManufacturer<T>(int id)
        {
            return this.manufacturersRepository.AllAsNoTracking().Where(x => x.Id == id).To<T>();
        }

        public IEnumerable<Item> GetAllApprovedProducts(int productId)
        {
            return this.manufacturersRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == productId).Products.Where(x => x.IsApproved).AsEnumerable();
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.manufacturersRepository.AllAsNoTracking().Select(x => new
            {
                x.Name,
                x.Id,
            }).ToList().Select(x => new KeyValuePair<string, string>(x.Name, x.Id.ToString())).AsEnumerable();
        }
    }
}
