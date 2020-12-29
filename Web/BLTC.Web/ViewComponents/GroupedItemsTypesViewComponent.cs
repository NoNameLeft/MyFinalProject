namespace BLTC.Web.ViewComponents
{
    using System.Threading.Tasks;

    using BLTC.Services.Data;
    using BLTC.Web.ViewModels.Items;
    using BLTC.Web.ViewModels.ViewComponents;
    using Microsoft.AspNetCore.Mvc;

    public class GroupedItemsTypesViewComponent : ViewComponent
    {
        private readonly IItemsService itemsService;

        public GroupedItemsTypesViewComponent(IItemsService itemsService)
        {
            this.itemsService = itemsService;
        }

        public IViewComponentResult Invoke()
        {
            var viewModel = new ItemsListViewModel<ItemsAllViewModel> { Items = this.itemsService.GetAllApprovedItems<ItemsAllViewModel>() };
            return this.View(viewModel);
        }
    }
}
