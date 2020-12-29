namespace BLTC.Web.ViewComponents
{
    using System.Threading.Tasks;

    using BLTC.Data.Models.Enums;
    using BLTC.Services.Data;
    using BLTC.Web.ViewModels.ViewComponents;
    using Microsoft.AspNetCore.Mvc;

    public class ItemsShapesViewComponent : ViewComponent
    {
        private readonly IItemsService itemsService;

        public ItemsShapesViewComponent(IItemsService itemsService)
        {
            this.itemsService = itemsService;
        }

        public IViewComponentResult Invoke()
        {
            var viewModel = new EnumsViewModel { KeyValuePairs = this.itemsService.GetKeyValuesOfEnum(typeof(ItemShape)) };
            return this.View(viewModel);
        }
    }
}
