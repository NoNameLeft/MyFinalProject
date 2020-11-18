namespace BLTC.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BLTC.Data.Common.Models;
    using BLTC.Data.Models.Enums;

    public class Item : BaseDeletableModel<int>
    {
        public Item()
        {
            this.Images = new HashSet<Image>();
            this.Orders = new HashSet<OrderItem>();
        }

        [Required]
        public int ManufacturerId { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public ItemType Type { get; set; }

        [Required]
        public ItemShape Shape { get; set; }

        [Required]
        public decimal Weight { get; set; }

        public decimal Purity { get; set; }

        public decimal Fineness { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string Dimensions { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<OrderItem> Orders { get; set; }
    }
}
