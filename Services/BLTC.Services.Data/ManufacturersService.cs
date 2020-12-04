namespace BLTC.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using BLTC.Data.Common.Repositories;
    using BLTC.Data.Models;

    public class ManufacturersService : IManufacturersService
    {
        private readonly IDeletableEntityRepository<Manufacturer> manufacturersRepository;

        public ManufacturersService(IDeletableEntityRepository<Manufacturer> manufacturersRepository)
        {
            this.manufacturersRepository = manufacturersRepository;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.manufacturersRepository.AllAsNoTracking().Select(x => new
            {
                x.Name,
                x.Id,
            }).ToList().Select(x => new KeyValuePair<string, string>(x.Name, x.Id.ToString()));
        }
    }
}
