namespace BLTC.Web.ViewModels.Items
{
    using System.Collections.Generic;

    using AutoMapper;
    using BLTC.Data.Models;
    using BLTC.Services.Mapping;

    public class ApprovedItemDetailsViewModel : IMapFrom<Item>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ManufacturerId { get; set; }

        public string ManufacturerName { get; set; }

        public Image ManufacturerLogo { get; set; }

        public decimal Weight { get; set; }

        public decimal Purity { get; set; }

        public int Fineness { get; set; }

        public string Dimensions { get; set; }

        public int Quanitity { get; set; }

        public string Description { get; set; }

        public ICollection<Image> Images { get; set; }

        public virtual void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Item, ApprovedItemDetailsViewModel>();
        }
    }
}
