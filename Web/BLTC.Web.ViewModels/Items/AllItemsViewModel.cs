namespace BLTC.Web.ViewModels.Items
{
    using System.Collections.Generic;

    using AutoMapper;
    using BLTC.Data.Models;
    using BLTC.Services.Mapping;

    public class AllItemsViewModel : IMapFrom<Item>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public ICollection<Image> Images { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Item, AllItemsViewModel>();
        }
    }
}
