namespace BLTC.Web.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using BLTC.Common;
    using BLTC.Services.Data;
    using BLTC.Services.Data.Models;
    using BLTC.Web.ViewModels.Manufacturers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    public class ManufacturersController : BaseController
    {
        private readonly IManufacturersService manufacturersService;
        private readonly ICountriesService countriesService;
        private readonly IUsersService usersService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ManufacturersController(
            IManufacturersService manufacturersService,
            ICountriesService countriesService,
            IUsersService usersService,
            IWebHostEnvironment webHostEnvironment)
        {
            this.manufacturersService = manufacturersService;
            this.countriesService = countriesService;
            this.usersService = usersService;
            this.webHostEnvironment = webHostEnvironment;
        }

        [Authorize]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Add(ManufacturersAddInputModel input)
        {
            await this.countriesService.Add(input.Name, input.IsoCode);

            var manufacturerDto = new CreateManufacturerDto
            {
                Name = input.Name,
                IsoCode = input.IsoCode,
                Overview = input.Overview,
                UserId = await this.usersService.GetIdAsync(this.User.Identity.Name),
            };

            await this.manufacturersService.AddAsync(manufacturerDto, input.Files, Path.Combine(this.webHostEnvironment.WebRootPath, "images", "logos"));

            return this.Redirect("/");
        }

        public async Task<IActionResult> Info(int manufacturerId)
        {
            var manufacturer = this.manufacturersService.GetById<ManufacturersInfoViewModel>(manufacturerId);
            var country = await this.countriesService.GetCountryById(manufacturer.CountryId);
            if (manufacturer is null)
            {
                return this.NotFound();
            }

            var viewModel = new ManufacturersInfoViewModel
            {
                Name = manufacturer.Name,
                Country = country.Name,
                Overview = manufacturer.Overview,
                Logo = manufacturer.Logo,
                Products = manufacturer.Products.Where(x => x.IsApproved).AsEnumerable(),
            };

            return this.View(viewModel);
        }
    }
}
