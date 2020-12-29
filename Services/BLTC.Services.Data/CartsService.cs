namespace BLTC.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BLTC.Data.Common.Repositories;
    using BLTC.Data.Models;
    using BLTC.Data.Models.Enums;
    using BLTC.Services.Data.Models;

    public class CartsService : ICartsService
    {
        private readonly IDeletableEntityRepository<ShoppingCart> cartsReposity;
        private readonly IDeletableEntityRepository<Item> itemsRepository;
        private readonly IDeletableEntityRepository<Order> ordersRepository;
        private readonly IDeletableEntityRepository<OrderItem> orderItemsRepository;
        private readonly IDeletableEntityRepository<ShoppingCart> shoppingCartsRepository;

        public CartsService(
            IDeletableEntityRepository<ShoppingCart> cartsReposity,
            IDeletableEntityRepository<Item> itemsRepository,
            IDeletableEntityRepository<Order> ordersRepository,
            IDeletableEntityRepository<OrderItem> orderItemsRepository,
            IDeletableEntityRepository<ShoppingCart> shoppingCartsRepository)
        {
            this.cartsReposity = cartsReposity;
            this.itemsRepository = itemsRepository;
            this.ordersRepository = ordersRepository;
            this.orderItemsRepository = orderItemsRepository;
            this.shoppingCartsRepository = shoppingCartsRepository;
        }

        public async Task AddItemToCart(int orderId, int itemId, string userId)
        {
            if (orderId == 0)
            {
                orderId = this.ordersRepository.All().FirstOrDefault(x => x.UserId == userId && x.Status == OrderStatus.InProgress).Id;
            }

            var item = this.itemsRepository.All().SingleOrDefault(x => x.Id == itemId);
            var cart = this.cartsReposity.All().SingleOrDefault(x => x.UserId == userId);

            var orderItem = new OrderItem
            {
                OrderId = orderId,
                ItemId = itemId,
                ApplicationUserId = userId,
            };

            cart.Items.Add(orderItem);

            await this.cartsReposity.SaveChangesAsync();
        }

        public async Task CreateAsync(string userId)
        {
            var cart = new ShoppingCart
            {
                UserId = userId,
            };

            await this.cartsReposity.AddAsync(cart);
            await this.cartsReposity.SaveChangesAsync();
        }

        public bool HasCart(string userId)
        {
            return this.cartsReposity.AllAsNoTracking().Any(x => x.UserId == userId);
        }

        public IEnumerable<CartsItemsDto> GetCartsItemsAsync(string userId)
        {
            var list = new CartsItemsListDto();

            var query = this.shoppingCartsRepository.All()
                .Join(this.ordersRepository.All(), sc => sc.UserId, o => o.UserId, (sc, o) => new { ShoppingCart = sc, Order = o })
                .Join(this.orderItemsRepository.All(), o => o.Order.Id, oi => oi.OrderId, (o, oi) => new { Order = o, OrderItem = oi })
                .Join(this.itemsRepository.All(), oi => oi.OrderItem.ItemId, i => i.Id, (oi, i) => new { OrderItem = oi, Item = i })
                .Where(sc => sc.OrderItem.OrderItem.Order.UserId == userId)
                .Select(i => new
                {
                    i.Item.Id,
                    i.Item.Name,
                    i.Item.Price,
                    i.Item.Images,
                    i.Item.Description,
                    CartId = i.OrderItem.Order.ShoppingCart.Id,
                });

            foreach (var item in query)
            {
                list.CartsItems.Add(new CartsItemsDto()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    CartId = item.CartId,
                    Description = item.Description,
                    Images = item.Images.Select(x => x.Name).First(),
                });
            }

            return list.CartsItems.AsEnumerable();
        }

        public string GetByUserId(string userId)
        {
            return this.cartsReposity.All().SingleOrDefault(x => x.UserId == userId).Id;
        }
    }
}
