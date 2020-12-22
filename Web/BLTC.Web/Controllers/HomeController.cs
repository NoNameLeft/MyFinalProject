namespace BLTC.Web.Controllers
{
    using System.Threading.Tasks;

    using BLTC.Services.Data;
    using BLTC.Web.ViewModels.Items;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IItemsService itemsService;

        public HomeController(IItemsService itemsService)
        {
            this.itemsService = itemsService;
        }

        public IActionResult Index()
        {
            var totalProducts = this.itemsService.GetAllApprovedItems<ItemsAllViewModel>();
            var viewModel = new ItemsAllListViewModel<ItemsAllViewModel> { Items = totalProducts };

            return this.View(viewModel);
        }

        public IActionResult GetByType()
        {
            return this.Redirect("/");
        }

        public IActionResult Privacy()
        {
            return this.View();
        }
    }
}
