namespace BLTC.Services.Data
{
    using System.Threading.Tasks;

    using BLTC.Data.Models;

    public interface ICountriesService
    {
        Task Add(string name, string isoCode);

        Task<Country> GetCountryById(int countryId);

        bool CheckIfEntityExistsByIsoCode(string isoCode);
    }
}
