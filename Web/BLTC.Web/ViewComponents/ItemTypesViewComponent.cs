namespace BLTC.Web.ViewComponents
{
    using BLTC.Data.Models.Enums;
    using BLTC.Services.Data;
    using BLTC.Web.ViewModels.ViewComponents;
    using Microsoft.AspNetCore.Mvc;

    public class ItemTypesViewComponent : ViewComponent
    {
        private readonly IItemsService itemsService;

        public ItemTypesViewComponent(IItemsService itemsService)
        {
            this.itemsService = itemsService;
        }

        public IViewComponentResult Invoke()
        {
            var viewModel = new ItemTypesViewModel
            {
                ItemsTypes = this.itemsService.GetKeyValuesOfEnum(typeof(ItemType)).Result,
            };

            return this.View(viewModel);
        }
    }
}
