namespace BLTC.Web.ViewModels.Items
{
    using System.Collections.Generic;
    using System.ComponentModel;

    using AutoMapper;
    using BLTC.Data.Models;
    using BLTC.Services.Mapping;

    public class ItemEditInputModel : ItemAddInputModel, IMapFrom<Item>, IHaveCustomMappings
    {
        private int itemId;
        private ICollection<Image> images;

        public ItemEditInputModel()
        {
            this.ItemId = this.itemId;
            this.Images = this.images;
        }

        public int ItemId
        {
            get { return this.itemId; }
            set { this.SetProperty(ref this.itemId, value); }
        }

        public ICollection<Image> Images
        {
            get { return this.images; }
            set { this.SetProperty(ref this.images, value); }
        }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Item, ItemEditInputModel>();
        }
    }
}
