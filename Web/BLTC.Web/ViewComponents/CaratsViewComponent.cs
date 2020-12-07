namespace BLTC.Web.ViewComponents
{
    using BLTC.Data.Models.Enums;
    using BLTC.Services.Data;
    using BLTC.Web.ViewModels.ViewComponents;
    using Microsoft.AspNetCore.Mvc;

    public class CaratsViewComponent : ViewComponent
    {
        private readonly IItemsService itemsService;

        public CaratsViewComponent(IItemsService itemsService)
        {
            this.itemsService = itemsService;
        }

        public IViewComponentResult Invoke()
        {
            var viewModel = new CaratsViewModel
            {
                Carats = this.itemsService.GetKeyValuesOfEnum(typeof(Carats)).Result,
            };

            return this.View(viewModel);
        }
    }
}
