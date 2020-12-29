namespace BLTC.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BLTC.Data.Common.Repositories;
    using BLTC.Data.Models;
    using BLTC.Services.Data.Models;
    using BLTC.Services.Mapping;

    public class ReceiptsService : IReceiptsService
    {
        private readonly IDeletableEntityRepository<Receipt> receiptsRepository;
        private readonly IDeletableEntityRepository<ShoppingCart> cartsRepository;
        private readonly IDeletableEntityRepository<Order> ordersRepository;
        private readonly IDeletableEntityRepository<OrderItem> orderItemsRepository;
        private readonly IDeletableEntityRepository<Item> itemsRepository;

        public ReceiptsService(
            IDeletableEntityRepository<Receipt> receiptsRepository,
            IDeletableEntityRepository<ShoppingCart> cartsRepository,
            IDeletableEntityRepository<Order> ordersRepository,
            IDeletableEntityRepository<OrderItem> orderItemsRepository,
            IDeletableEntityRepository<Item> itemsRepository)
        {
            this.receiptsRepository = receiptsRepository;
            this.cartsRepository = cartsRepository;
            this.ordersRepository = ordersRepository;
            this.orderItemsRepository = orderItemsRepository;
            this.itemsRepository = itemsRepository;
        }

        public async Task CreateReceipt(string userId)
        {
            var cart = this.cartsRepository.All().SingleOrDefault(x => x.UserId == userId);

            var orderItems = this.orderItemsRepository.All().Where(x => x.ShoppingCartId == cart.Id).ToList();

            if (!this.receiptsRepository.All().Any(x => x.OrderId == orderItems.First().OrderId))
            {
                var receipt = new Receipt()
                {
                    CustomerId = userId,
                    OrderId = orderItems.First().OrderId,
                };

                foreach (var item in orderItems)
                {
                    receipt.OrderItems.Add(item);
                }

                await this.receiptsRepository.AddAsync(receipt);
                await this.receiptsRepository.SaveChangesAsync();
            }
        }

        public IEnumerable<ReceiptsDto> GetReceiptByCustomerId(string userId, string orderNumber)
        {
            var list = new ReceiptsListDto();
            var query = this.orderItemsRepository.AllAsNoTrackingWithDeleted()
                .Join(this.ordersRepository.AllAsNoTrackingWithDeleted(), oi => oi.OrderId, o => o.Id, (oi, o) => new { OrderItem = oi, Order = o })
                .Join(this.itemsRepository.AllAsNoTrackingWithDeleted(), oi => oi.OrderItem.ItemId, i => i.Id, (oi, i) => new { OrderItem = oi, Item = i })
                .Where(oi => oi.OrderItem.OrderItem.ApplicationUserId == userId && oi.OrderItem.Order.Number == orderNumber)
                .Select(i => new
                {
                    i.OrderItem.Order.OrderedOn,
                    i.OrderItem.Order.Number,
                    i.Item.Name,
                    i.Item.Price,
                }).ToList();

            var grouped = query.GroupBy(x => new { x.OrderedOn, x.Number, x.Name, x.Price });

            foreach (var r in grouped)
            {
                var amount = query.Where(x => x.Name == r.Key.Name).Count();
                list.Receipts.Add(new ReceiptsDto()
                {
                    OrderedDate = r.Key.OrderedOn,
                    OrderNumber = r.Key.Number,
                    ItemName = r.Key.Name,
                    Price = r.Key.Price,
                    Count = amount,
                    TotalItemPrice = r.Key.Price * amount,
                });
            }

            return list.Receipts.AsEnumerable();
        }
    }
}
