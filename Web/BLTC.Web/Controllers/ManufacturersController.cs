namespace BLTC.Web.Controllers
{
    using System.Linq;

    using BLTC.Services.Data;
    using BLTC.Web.ViewModels.Manufacturers;
    using Microsoft.AspNetCore.Mvc;

    public class ManufacturersController : Controller
    {
        private readonly IManufacturersService manufacturersService;

        public ManufacturersController(IManufacturersService manufacturersService)
        {
            this.manufacturersService = manufacturersService;
        }

        public IActionResult Info(int manufacturerId)
        {
            var manufacturer = this.manufacturersService.GetById<ManufacturersInfoViewModel>(manufacturerId);
            if (manufacturer is null)
            {
                return this.NotFound();
            }

            var viewModel = new ManufacturersInfoViewModel
            {
                Name = manufacturer.Name,
                Country = manufacturer.Country,
                Overview = manufacturer.Overview,
                Logo = manufacturer.Logo,
                Products = manufacturer.Products.Where(x => x.IsApproved).AsEnumerable(),
            };

            return this.View(viewModel);
        }
    }
}
