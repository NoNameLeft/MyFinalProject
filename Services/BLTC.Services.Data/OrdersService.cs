namespace BLTC.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using BLTC.Data.Common.Repositories;
    using BLTC.Data.Models;
    using BLTC.Data.Models.Enums;
    using BLTC.Services.Data.Models;
    using BLTC.Services.Mapping;
    using Microsoft.Extensions.Configuration;

    public class OrdersService : IOrdersService
    {
        private readonly Random random;
        private readonly int minValue = 1000000;
        private readonly int maxValue = 9999999;

        private readonly IDeletableEntityRepository<Order> ordersRepository;
        private readonly IDeletableEntityRepository<OrderItem> orderItemsRepository;
        private readonly IDeletableEntityRepository<Item> itemsRepository;
        private readonly IDeletableEntityRepository<ShippingDetails> shippingRepository;
        private readonly IConfiguration configuration;

        public OrdersService(
            IDeletableEntityRepository<Order> ordersRepository,
            IDeletableEntityRepository<OrderItem> orderItemsRepository,
            IDeletableEntityRepository<Item> itemsRepository,
            IDeletableEntityRepository<ShippingDetails> shippingRepository,
            IConfiguration configuration)
        {
            this.ordersRepository = ordersRepository;
            this.orderItemsRepository = orderItemsRepository;
            this.itemsRepository = itemsRepository;
            this.shippingRepository = shippingRepository;
            this.configuration = configuration;
            this.random = new Random();
        }

        public async Task<int> CreateAsync(string userId)
        {
            var order = new Order
            {
                UserId = userId,
                Number = this.UniqueOrderNumber(this.random.Next(this.minValue, this.maxValue)),
            };

            await this.ordersRepository.AddAsync(order);
            await this.ordersRepository.SaveChangesAsync();

            return order.Id;
        }

        public IEnumerable<CheckoutItemsDto> GetCartItems(string cartId)
        {
            var list = new CheckoutItemsListDto();

            var result = this.orderItemsRepository.All()
                .Join(this.itemsRepository.All(), oi => oi.ItemId, i => i.Id, (oi, i) => new { OrderItem = oi, Item = i })
                .Where(oi => oi.OrderItem.ShoppingCartId == cartId)
                .Select(i => new
                {
                    i.Item.Id,
                    i.Item.Name,
                    i.Item.Price,
                }).ToList();

            var grouped = result.GroupBy(x => new { x.Id, x.Name, x.Price }).ToHashSet();
            foreach (var item in grouped)
            {
                var i = new CheckoutItemsDto()
                {
                    Id = item.Key.Id,
                    Name = item.Key.Name,
                    Price = item.Key.Price,
                    Count = this.orderItemsRepository.All().Where(x => x.ItemId == item.Key.Id).Count(),
                };
                list.CheckoutItems.Add(i);
            }

            return list.CheckoutItems.AsEnumerable();
        }

        public void RemoveItemFromCart(int itemId, string cartId)
        {
            var cartItem = this.orderItemsRepository.All().Where(x => x.ShoppingCartId == cartId).FirstOrDefault(x => x.ItemId == itemId);
            if (cartItem != null)
            {
                string queryString = @"DELETE FROM [dbo].[OrdersItems] WHERE [ItemId] = @id AND [ShoppingCartId] = @id2";
                string connectionString = this.configuration.GetConnectionString("DefaultConnection");
                using SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@id", itemId);
                command.Parameters.AddWithValue("@id2", cartId);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            else
            {
                throw new Exception($"You don't have that item in your cart.");
            }
        }

        public async Task ClearCartAsync(string cartId, string orderNumber)
        {
            var order = this.ordersRepository.All().SingleOrDefault(x => x.Number == orderNumber);
            order.Status = OrderStatus.Packing;
            this.ordersRepository.Delete(order);

            var cartItems = this.orderItemsRepository.All().Where(x => x.ShoppingCartId == cartId).ToList();
            while (this.orderItemsRepository.All().Where(x => x.ShoppingCartId == cartId).Count() > 0)
            {
                foreach (var item in cartItems)
                {
                    if (item != null)
                    {
                        this.orderItemsRepository.Delete(item);
                        await this.orderItemsRepository.SaveChangesAsync();
                    }
                }
            }
        }

        public async Task DecreaseItemAmount(int itemId)
        {
            var item = this.orderItemsRepository.All().Where(x => x.ItemId == itemId).FirstOrDefault();

            this.orderItemsRepository.HardDelete(item);
            await this.orderItemsRepository.SaveChangesAsync();
        }

        public bool IsFinished(string userId)
        {
            var order = this.ordersRepository.AllAsNoTracking().SingleOrDefault(x => x.UserId == userId);

            if (order is null)
            {
                return true;
            }
            else if (order.Status != OrderStatus.InProgress)
            {
                return true;
            }

            return false;
        }

        public async Task AddShippingInfo(string userId, string address, string city, string state, int zipCode)
        {
            var order = this.ordersRepository.All().SingleOrDefault(x => x.UserId == userId);
            if (order.ShippingId is null || order.ShippingId <= 0)
            {
                var shippingDetails = new ShippingDetails()
                {
                    Address = address,
                    City = city,
                    State = state,
                    ZipCode = zipCode,
                };
                await this.shippingRepository.AddAsync(shippingDetails);
                order.Shipping = shippingDetails;
                await this.shippingRepository.SaveChangesAsync();
            }
        }

        public IEnumerable<UsersOrdersDto> GetUsersOrders(string userId)
        {
            var list = new UsersOrdersListDto();
            var orders = this.ordersRepository.AllAsNoTrackingWithDeleted()
                .Join(this.shippingRepository.All(), o => o.ShippingId, s => s.Id, (o, s) => new { Order = o, ShippingDetails = s })
                .Where(x => x.Order.UserId == userId && x.Order.Status != OrderStatus.InProgress)
                .ToList();

            foreach (var order in orders)
            {
                list.UsersOrders.Add(new UsersOrdersDto
                {
                    Address = order.ShippingDetails.Address,
                    City = order.ShippingDetails.City,
                    ZipCode = order.ShippingDetails.ZipCode,
                    Number = order.Order.Number,
                    Status = order.Order.Status,
                });
            }

            return list.UsersOrders.AsEnumerable();
        }

        private string UniqueOrderNumber(int random)
        {
            var allOrders = this.ordersRepository.AllAsNoTracking().ToList();
            if (allOrders.Count() > 0)
            {
                do
                {
                    random = this.random.Next(this.minValue, this.maxValue);
                }
                while (allOrders.Any(x => this.CustomIntParser(x.Number) == random));
            }

            return Convert.ToString(random);
        }

        private int CustomIntParser(string word)
        {
            var num = 0;
            for (int i = 0; i < word.Length; i++)
            {
                num = (num * 10) + (word[i] - '0');
            }

            return num;
        }
    }
}
