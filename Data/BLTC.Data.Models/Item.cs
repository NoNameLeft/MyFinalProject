namespace BLTC.Data.Models
{
    using System.Collections.Generic;

    using BLTC.Data.Common.Models;
    using BLTC.Data.Models.Enums;

    public class Item : BaseDeletableModel<int>
    {
        public Item()
        {
            this.Images = new HashSet<Image>();
        }

        public int ManufacturerId { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public string Name { get; set; }

        public ItemType Type { get; set; }

        public ItemShape Shape { get; set; }

        public decimal Weight { get; set; }

        public decimal Purity { get; set; }

        public decimal Fineness { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string Dimensions { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
