namespace BLTC.Services.Data
{
    using System.Threading.Tasks;

    using BLTC.Data.Models;

    public interface ICountriesService
    {
        Task<Country> GetCountryById(int countryId);
    }
}
