namespace BLTC.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BLTC.Data.Common.Models;

    public class ShippingDetails : BaseDeletableModel<int>
    {
        public ShippingDetails()
        {
            this.Orders = new HashSet<Order>();
        }

        [Required]
        [MinLength(10, ErrorMessage = "Shipping address cannot be less than 10 characters.")]
        public string Address { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Shipping's name of the city or the town cannot be less 5 characters.")]
        public string City { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "State's name shouldn't be less than 5 characters.")]
        public string State { get; set; }

        [Required]
        [Range(4, 10, ErrorMessage = "Zip code should be between 4 and 10 characters.")]
        public int ZipCode { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
