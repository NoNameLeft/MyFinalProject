namespace BLTC.Web.ViewModels.Orders
{
    using System.ComponentModel.DataAnnotations;

    public class ShippingInfoInputModel : BaseOrderInputModel
    {
        [Required]
        [MinLength(10, ErrorMessage = "Shipping address cannot be less than 10 characters.")]
        [Display(Name = "Address")]
        public string ShippingAddress { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Shipping's name of the city or the town cannot be less 5 characters.")]
        [Display(Name = "City")]
        public string ShippingTown { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "State's name shouldn't be less than 5 characters.")]
        [Display(Name = "State")]
        public string ShippingState { get; set; }

        [Required]
        [Range(4, 10, ErrorMessage = "Zip code should be between 4 and 10 characters.")]
        [Display(Name = "Zip")]
        public int ShippingZipCode { get; set; }
    }
}
