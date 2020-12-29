namespace BLTC.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using BLTC.Services.Data;
    using BLTC.Web.ViewModels.Orders;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Stripe;

    public class OrdersController : BaseController
    {
        private readonly string zipCodePattern = @"^(\d{4,10})$";

        private readonly ICartsService cartsService;
        private readonly IUsersService usersService;
        private readonly IOrdersService ordersService;
        private readonly IReceiptsService receiptsService;

        public OrdersController(
            ICartsService cartsService,
            IUsersService usersService,
            IOrdersService ordersService,
            IReceiptsService receiptsService)
        {
            this.cartsService = cartsService;
            this.usersService = usersService;
            this.ordersService = ordersService;
            this.receiptsService = receiptsService;
        }

        [Authorize]
        [Route("orders/info/your_order")]
        public IActionResult UsersOrders()
        {
            return this.View();
        }

        [Authorize]
        [Route("/your_cart/actions/checkout")]
        public IActionResult Checkout()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Charge(BillingInfoInputModel input)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = input.Email,
                Source = input.Token,
                Shipping = new ShippingOptions
                {
                    Name = input.FullName,
                    Phone = input.Phone,
                    Address = new AddressOptions
                    {
                        Line1 = input.Address,
                        Line2 = input.ShippingAddress,
                        PostalCode = input.ShippingZipCode.ToString(),
                        State = input.ShippingState,
                        City = input.ShippingTown,
                    },
                },
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = Convert.ToInt32(input.TotalPrice) * 100,
                Description = $"{this.User.Identity.Name} bought {input} products.",
                Currency = "eur",
                Customer = customer.Id,
                ReceiptEmail = input.Email,
            });

            if (charge.Status == "succeeded")
            {
                var viewModel = new ReceiptsListViewModel();
                var userId = await this.usersService.GetIdAsync(this.User.Identity.Name);
                await this.receiptsService.CreateReceipt(userId);
                await this.ordersService.AddShippingInfo(
                        userId,
                        string.IsNullOrEmpty(input.ShippingAddress) ? input.Address : input.ShippingAddress,
                        string.IsNullOrEmpty(input.ShippingTown) ? input.City : input.ShippingTown,
                        string.IsNullOrEmpty(input.ShippingState) ? input.State : input.ShippingState,
                        !Regex.IsMatch(Convert.ToString(input.ShippingZipCode), this.zipCodePattern) ? input.ZipCode : input.ShippingZipCode);

                var receipt = this.receiptsService.GetReceiptByCustomerId(userId, await this.usersService.GetOrderNumber(this.User.Identity.Name));

                foreach (var r in receipt)
                {
                    viewModel.Receipts.Add(new ReceiptViewModel
                    {
                        Count = r.Count,
                        Price = r.Price,
                        ItemName = r.ItemName,
                        OrderNumber = r.OrderNumber,
                        OrderedDate = r.OrderedDate,
                        TotalItemPrice = r.TotalItemPrice,
                    });
                }

                await this.ordersService.ClearCartAsync(this.cartsService.GetByUserId(userId), await this.usersService.GetOrderNumber(this.User.Identity.Name));
                return this.View("Receipt", viewModel);
            }

            return this.Redirect("/");
        }

        [Authorize]
        public async Task<IActionResult> Receipt(string orderNumber)
        {
            var viewModel = new ReceiptsListViewModel();
            var receipts = this.receiptsService.GetReceiptByCustomerId(await this.usersService.GetIdAsync(this.User.Identity.Name), orderNumber);
            foreach (var r in receipts)
            {
                viewModel.Receipts.Add(new ReceiptViewModel
                {
                    Count = r.Count,
                    Price = r.Price,
                    ItemName = r.ItemName,
                    OrderNumber = r.OrderNumber,
                    OrderedDate = r.OrderedDate,
                    TotalItemPrice = r.TotalItemPrice,
                });
            }

            return this.View(viewModel);
        }

        [Authorize]
        [Route("info/carts/your_cart")]
        public async Task<IActionResult> Cart()
        {
            var userId = await this.usersService.GetIdAsync(this.User.Identity.Name);
            var currentCart = this.cartsService.GetCartsItemsAsync(userId);
            var cartItemsViewModel = new OrderItemsListViewModel();

            foreach (var cartItem in currentCart)
            {
                cartItemsViewModel.CartItems.Add(new OrderItemViewModel()
                {
                    Id = cartItem.Id,
                    Name = cartItem.Name,
                    Price = cartItem.Price,
                    CartId = cartItem.CartId,
                    ImagePaths = cartItem.Images,
                    Description = cartItem.Description,
                });
            }

            return this.View(cartItemsViewModel);
        }

        [Authorize]
        public async Task<IActionResult> AddToCart(int itemId)
        {
            if (string.IsNullOrEmpty(this.User.Identity.Name))
            {
                return this.RedirectToAction("Details", new { itemId });
            }

            var userId = await this.usersService.GetIdAsync(this.User.Identity.Name);

            if (!this.cartsService.HasCart(userId))
            {
                await this.cartsService.CreateAsync(userId);
            }

            int orderId = 0;
            if (this.ordersService.IsFinished(userId))
            {
                _ = await this.ordersService.CreateAsync(userId);
            }

            await this.cartsService.AddItemToCart(orderId, itemId, userId);

            return this.RedirectToAction("Details", "Items", new { itemId });
        }

        [Authorize]
        public async Task<IActionResult> IncreaseQuantity(int itemId)
        {
            await this.cartsService.AddItemToCart(0, itemId, await this.usersService.GetIdAsync(this.User.Identity.Name));
            return this.RedirectToAction("Cart", "Orders");
        }

        [Authorize]
        public IActionResult RemoveFromCart(int itemId, string cartId)
        {
            this.ordersService.RemoveItemFromCart(itemId, cartId);
            return this.RedirectToAction("Cart", "Orders");
        }

        [Authorize]
        public async Task<IActionResult> DecreaseQuanitity(int itemId)
        {
            await this.ordersService.DecreaseItemAmount(itemId);
            return this.RedirectToAction("Cart", "Orders");
        }
    }
}
