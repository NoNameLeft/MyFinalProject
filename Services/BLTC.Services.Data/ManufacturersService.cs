namespace BLTC.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BLTC.Data.Common.Repositories;
    using BLTC.Data.Models;

    public class ManufacturersService : IManufacturersService
    {
        private readonly IDeletableEntityRepository<Manufacturer> manufacturersRepository;

        public ManufacturersService(IDeletableEntityRepository<Manufacturer> manufacturersRepository)
        {
            this.manufacturersRepository = manufacturersRepository;
        }

        public async Task<IEnumerable<KeyValuePair<string, string>>> GetAllAsKeyValuePairs()
        {
            return await Task.FromResult(this.manufacturersRepository.AllAsNoTracking().Select(x => new
            {
                x.Name,
                x.Id,
            }).ToList().Select(x => new KeyValuePair<string, string>(x.Name, x.Id.ToString())));
        }

        public async Task<int> GetIdByName(string name)
        {
            return await Task.FromResult<int>(this.manufacturersRepository.AllAsNoTracking().FirstOrDefault(x => x.Name == name).Id);
        }
    }
}
