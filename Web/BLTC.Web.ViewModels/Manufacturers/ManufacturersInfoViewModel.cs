namespace BLTC.Web.ViewModels.Manufacturers
{
    using System.Collections.Generic;

    using AutoMapper;
    using BLTC.Data.Models;
    using BLTC.Services.Mapping;

    public class ManufacturersInfoViewModel : IMapFrom<Manufacturer>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CountryId { get; set; }

        public string Country { get; set; }

        public string Overview { get; set; }

        public Image Logo { get; set; }

        public IEnumerable<Item> Products { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Manufacturer, ManufacturersInfoViewModel>();
        }
    }
}
