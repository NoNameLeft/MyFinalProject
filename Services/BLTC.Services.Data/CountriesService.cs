namespace BLTC.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using BLTC.Data.Common.Repositories;
    using BLTC.Data.Models;

    public class CountriesService : ICountriesService
    {
        private readonly IDeletableEntityRepository<Country> countriesRepository;

        public CountriesService(IDeletableEntityRepository<Country> countriesRepository)
        {
            this.countriesRepository = countriesRepository;
        }

        public async Task<Country> GetCountryById(int countryId)
        {
            return await Task.FromResult(this.countriesRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == countryId));
        }
    }
}
