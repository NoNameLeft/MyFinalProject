namespace BLTC.Web.ViewModels.Items
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class ItemBaseInputModel
    {
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string Name { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        public int Shape { get; set; }

        [Required]
        [Range(1, 12500)]
        public decimal Weight { get; set; }

        [Required]
        [Display(Name = "Precious metal content (grams):")]
        public decimal Purity { get; set; }

        [Required]
        public int Fineness { get; set; }

        [Required]
        [Range(1, 1000)]
        public int Quantity { get; set; }

        [Required]
        public string Dimensions { get; set; }

        [Required]
        [MinLength(100)]
        public string Description { get; set; }

        [Required]
        public virtual IFormFileCollection Files { get; set; }
    }
}
