namespace BLTC.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    using BLTC.Data.Common.Repositories;
    using BLTC.Data.Models;
    using BLTC.Data.Models.Enums;
    using BLTC.Services.Mapping;

    public class ItemsService : IItemsService
    {
        private readonly IDeletableEntityRepository<Item> itemsRepository;

        public ItemsService(IDeletableEntityRepository<Item> itemsRepository)
        {
            this.itemsRepository = itemsRepository;
        }

        public async Task<int> Add(string name, int type, int shape, decimal weight, decimal purity, int fineness, int quantity, string dimensions, string description, int manufacturer)
        {
            // if (this.itemsRepository.AllAsNoTracking().Any(x => x.Name == name))
            // {
            //     // do something...
            // }
            var item = new Item()
            {
                Name = name,
                Type = (ItemType)Enum.ToObject(typeof(ItemType), type),
                Shape = (ItemShape)Enum.ToObject(typeof(ItemShape), shape),
                Weight = weight,
                Purity = purity,
                Fineness = (Carats)Enum.ToObject(typeof(Carats), fineness),
                Price = this.CalculateItemPrice(),
                Quantity = quantity,
                Dimensions = dimensions,
                Description = description,
                ManufacturerId = manufacturer,
            };

            await this.itemsRepository.AddAsync(item);
            await this.itemsRepository.SaveChangesAsync();

            return item.Id;
        }

        public async void AddImagesToItem(List<Image> images, int itemId)
        {
            var item = await this.GetItemById(itemId);
            images.ForEach(i => item.Images.Add(i));
        }

        public Task<int> GetIdByName(string name)
        {
            return Task.FromResult(this.itemsRepository.AllAsNoTracking().FirstOrDefault(x => x.Name == name).Id);
        }

        public Task<Item> GetItemById(int itemId)
        {
            return Task.FromResult(this.itemsRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == itemId));
        }

        public IEnumerable<T> GetAllItems<T>()
        {
            return this.itemsRepository.All().To<T>().ToList();
        }

        public IEnumerable<T> GetItem<T>(int itemId)
        {
            return this.itemsRepository.All().Where(x => x.Id == itemId).To<T>();
        }

        public IEnumerable<KeyValuePair<string, string>> GetKeyValuesOfEnum(Type enumType)
        {
            var displayNames = this.GetDisplayNames(enumType);
            var names = enumType.GetEnumNames();
            var values = enumType.GetEnumValues().Cast<int>().ToList();
            if (displayNames.Count > 0)
            {
                return names.Select((n, index) =>
                             new KeyValuePair<string, string>(displayNames[index], values[index].ToString()));
            }

            return names.Select((n, index) =>
                             new KeyValuePair<string, string>(n, values[index].ToString()));
        }

        private decimal CalculateItemPrice()
        {
            // Implement method which calls web api and calculate the current price of the metals.
            // Add disclamer based on what you calculate the price
            var dailyGoldPricePerGram = 50.0m;
            var currentPrice = dailyGoldPricePerGram + (dailyGoldPricePerGram * 0.33m);

            return currentPrice;
        }

        private List<string> GetDisplayNames(Type type)
        {
            var displaynames = new List<string>();
            var names = Enum.GetNames(type);
            foreach (var name in names)
            {
                var field = type.GetField(name);
                var fds = field.GetCustomAttributes(typeof(DisplayAttribute), true);

                if (fds.Length == 0)
                {
                    displaynames.Add(name);
                }

                foreach (DisplayAttribute fd in fds)
                {
                    displaynames.Add(fd.Name);
                }
            }

            return displaynames;
        }
    }
}
