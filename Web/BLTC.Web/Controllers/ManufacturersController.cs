namespace BLTC.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using BLTC.Services.Data;
    using BLTC.Web.ViewModels.Manufacturers;
    using Microsoft.AspNetCore.Mvc;

    public class ManufacturersController : Controller
    {
        private readonly IManufacturersService manufacturersService;
        private readonly ICountriesService countriesService;

        public ManufacturersController(IManufacturersService manufacturersService, ICountriesService countriesService)
        {
            this.manufacturersService = manufacturersService;
            this.countriesService = countriesService;
        }

        public async Task<IActionResult> Info(int manufacturerId)
        {
            var manufacturer = this.manufacturersService.GetManufacturer<ManufacturersInfoViewModel>(manufacturerId).FirstOrDefault();
            var country = await this.countriesService.GetCountryById(manufacturer.CountryId);
            var products = this.manufacturersService.GetAllProducts(manufacturerId);

            var viewModel = new ManufacturersInfoViewModel
            {
                Name = manufacturer.Name,
                Country = country.Name,
                Overview = manufacturer.Overview,
                Logo = manufacturer.Logo,
                Products = products,
            };

            return this.View(viewModel);
        }
    }
}
