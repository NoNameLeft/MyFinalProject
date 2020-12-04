namespace BLTC.Web.ViewComponents
{
    using BLTC.Data.Models.Enums;
    using BLTC.Services.Data;
    using BLTC.Web.ViewModels.ViewComponents;
    using Microsoft.AspNetCore.Mvc;

    public class ItemShapesViewComponent : ViewComponent
    {
        private readonly IItemsService itemsService;

        public ItemShapesViewComponent(IItemsService itemsService)
        {
            this.itemsService = itemsService;
        }

        public IViewComponentResult Invoke()
        {
            var viewModel = new ItemShapesViewModel
            {
                ItemsShapes = this.itemsService.GetKeyValuesOfEnum(typeof(ItemShape)),
            };

            return this.View(viewModel);
        }
    }
}
