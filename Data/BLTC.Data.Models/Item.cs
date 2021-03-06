﻿namespace BLTC.Data.Models
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
        [MinLength(6, ErrorMessage = "Item's name cannot be less than 5 characters long.")]
        public string Name { get; set; }

        [Required]
        public ItemType Type { get; set; }

        [Required]
        public ItemShape Shape { get; set; }

        [Required]
        [Range(1, 12500, ErrorMessage = "Weight should be between 1 and 12500 grams.")]
        public decimal Weight { get; set; }

        [Required]
        public decimal Purity { get; set; }

        [Required]
        public Carats Fineness { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Dimensions cannot be less than 5 characters.")]
        public string Dimensions { get; set; }

        [Required]
        [MinLength(50, ErrorMessage = "Item's description should be at least 50 characters long.")]
        public string Description { get; set; }

        public bool IsApproved { get; set; } = false;

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<OrderItem> Orders { get; set; }
    }
}
