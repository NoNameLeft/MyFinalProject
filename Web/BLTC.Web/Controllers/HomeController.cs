namespace BLTC.Web.Controllers
{
    using BLTC.Services.Data;
    using BLTC.Web.ViewModels.Items;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IItemsService itemsService;

        public HomeController(IItemsService itemsService)
        {
            this.itemsService = itemsService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var totalProducts = this.itemsService.GetAllApprovedItems<ItemsAllViewModel>();
            var viewModel = new ItemsAllListViewModel<ItemsAllViewModel> { Items = totalProducts };

            return this.View(viewModel);
        }

        [AllowAnonymous]
        [Route("/site/forms/privacy")]
        public IActionResult Privacy()
        {
            return this.View();
        }
    }
}
