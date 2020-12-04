namespace BLTC.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;

    using BLTC.Data.Common.Repositories;
    using BLTC.Data.Models;
    using BLTC.Data.Models.Enums;

    public class ItemsService : IItemsService
    {
        private readonly IDeletableEntityRepository<Item> itemsRepository;

        public ItemsService(IDeletableEntityRepository<Item> itemsRepository)
        {
            this.itemsRepository = itemsRepository;
        }

        public async void Add(string name, int type, int shape, decimal weight, decimal purity, int fineness, int quantity, string dimensions, string description, int manufacturer)
        {
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

            // await this.itemsRepository.SaveChangesAsync();
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
            return 0;
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
