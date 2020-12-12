namespace BLTC.Web.ViewModels.Administration.Items
{
    using System.Collections.Generic;

    using AutoMapper;
    using BLTC.Data.Models;
    using BLTC.Services.Mapping;
    using BLTC.Web.ViewModels.Items;
    using Microsoft.AspNetCore.Http;

    public class EditItemInputModel : ItemBaseInputModel, IMapFrom<Item>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public int ManufacturerId { get; set; }

        public override IFormFileCollection Files { get; set; }

        public ICollection<Image> Images { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Item, EditItemInputModel>();
        }
    }
}
