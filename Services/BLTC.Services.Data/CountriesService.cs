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

        public async Task Add(string name, string isoCode)
        {
            if (!this.countriesRepository.All().Any(x => x.IsoCode == isoCode || x.Name == name))
            {
                var country = new Country
                {
                    Name = name,
                    IsoCode = isoCode,
                };

                await this.countriesRepository.AddAsync(country);
                await this.countriesRepository.SaveChangesAsync();
            }
        }

        public async Task<Country> GetCountryById(int countryId)
        {
            return await Task.FromResult(this.countriesRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == countryId));
        }

        public bool CheckIfEntityExistsByIsoCode(string isoCode)
        {
            return this.countriesRepository.AllAsNoTrackingWithDeleted().Any(x => x.IsoCode == isoCode);
        }
    }
}
