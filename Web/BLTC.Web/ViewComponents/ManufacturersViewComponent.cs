namespace BLTC.Web.ViewComponents
{
    using BLTC.Services.Data;
    using BLTC.Web.ViewModels.ViewComponents;
    using Microsoft.AspNetCore.Mvc;

    public class ManufacturersViewComponent : ViewComponent
    {
        private readonly IManufacturersService manufacturersService;

        public ManufacturersViewComponent(IManufacturersService manufacturersService)
        {
            this.manufacturersService = manufacturersService;
        }

        public IViewComponentResult Invoke()
        {
            var viewModel = new ManufacturersViewModel
            {
                Manufacturers = this.manufacturersService.GetAllAsKeyValuePairs(),
            };

            return this.View(viewModel);
        }
    }
}
