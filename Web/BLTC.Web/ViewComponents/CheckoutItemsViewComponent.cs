namespace BLTC.Web.ViewComponents
{
    using System.Threading.Tasks;

    using BLTC.Services.Data;
    using BLTC.Web.ViewModels.ViewComponents;
    using Microsoft.AspNetCore.Mvc;

    public class CheckoutItemsViewComponent : ViewComponent
    {
        private readonly IOrdersService ordersService;
        private readonly IUsersService usersService;
        private readonly ICartsService cartsService;

        public CheckoutItemsViewComponent(
            IOrdersService ordersService,
            IUsersService usersService,
            ICartsService cartsService)
        {
            this.ordersService = ordersService;
            this.usersService = usersService;
            this.cartsService = cartsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = await this.usersService.GetIdAsync(this.User.Identity.Name);
            var cartId = this.cartsService.GetByUserId(userId);
            var cartItems = this.ordersService.GetCartItems(cartId);
            var viewModel = new CheckoutItemsListViewModel();

            foreach (var item in cartItems)
            {
                viewModel.CartItems.Add(new CheckoutItemsViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    Count = item.Count,
                });
            }

            return this.View(viewModel);
        }
    }
}
