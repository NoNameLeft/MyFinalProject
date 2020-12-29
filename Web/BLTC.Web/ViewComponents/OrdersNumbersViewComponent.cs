namespace BLTC.Web.ViewComponents
{
    using System.Threading.Tasks;

    using BLTC.Services.Data;
    using BLTC.Web.ViewModels.ViewComponents;
    using Microsoft.AspNetCore.Mvc;

    public class OrdersNumbersViewComponent : ViewComponent
    {
        private readonly IOrdersService ordersService;
        private readonly IUsersService usersService;

        public OrdersNumbersViewComponent(IOrdersService ordersService, IUsersService usersService)
        {
            this.ordersService = ordersService;
            this.usersService = usersService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = new OrdersNumbersListViewModel();

            var userId = await this.usersService.GetIdAsync(this.User.Identity.Name);
            var orders = this.ordersService.GetUsersOrders(userId);
            foreach (var o in orders)
            {
                viewModel.Orders.Add(new OrdersNumbersViewModel
                {
                    Address = o.Address,
                    City = o.City,
                    ZipCode = o.ZipCode,
                    Number = o.Number,
                    Status = o.Status,
                });
            }

            return this.View(viewModel);
        }
    }
}
