namespace BLTC.Web.ViewComponents
{
    using System.Threading.Tasks;

    using BLTC.Data.Models.Enums;
    using BLTC.Services.Data;
    using BLTC.Web.ViewModels.ViewComponents;
    using Microsoft.AspNetCore.Mvc;

    public class ItemsTypesViewComponent : ViewComponent
    {
        private readonly IItemsService itemsService;

        public ItemsTypesViewComponent(IItemsService itemsService)
        {
            this.itemsService = itemsService;
        }

        public IViewComponentResult Invoke()
        {
            var viewModel = new EnumsViewModel { KeyValuePairs = this.itemsService.GetKeyValuesOfEnum(typeof(ItemType)) };
            return this.View(viewModel);
        }
    }
}
