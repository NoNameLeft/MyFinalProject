namespace BLTC.Web.Controllers
{
    using BLTC.Common;
    using BLTC.Services.Data;
    using BLTC.Web.ViewModels.Countries;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class CountriesController : BaseController
    {
        private readonly ICountriesService countriesService;

        public CountriesController(ICountriesService countriesService)
        {
            this.countriesService = countriesService;
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Add(CountryAddInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }
            else if (this.countriesService.CheckIfEntityExistsByIsoCode(input.IsoCode))
            {
                return this.StatusCode(409, $"Contry with ISO Code '{input.IsoCode}' already exists.");
            }

            this.countriesService.Add(input.Name, input.IsoCode);
            return this.Redirect("/");
        }
    }
}
