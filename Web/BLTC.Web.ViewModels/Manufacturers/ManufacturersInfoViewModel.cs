namespace BLTC.Web.ViewModels.Manufacturers
{
    using System.Collections.Generic;

    using AutoMapper;
    using BLTC.Data.Models;
    using BLTC.Services.Mapping;

    public class ManufacturersInfoViewModel : BaseManufacturerViewModel, IMapFrom<Manufacturer>, IHaveCustomMappings
    {
        public IEnumerable<Item> Products { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Manufacturer, ManufacturersInfoViewModel>();
        }
    }
}
