namespace BLTC.Web.ViewModels.Orders
{
    using System.ComponentModel.DataAnnotations;

    public class BillingInfoInputModel : ShippingInfoInputModel
    {
        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        [RegularExpression(@"^(\d{4,10})$", ErrorMessage = "Zip code should be between 4 and 10 digits long.")]
        [Display(Name = "Zip")]
        public int ZipCode { get; set; }

        [Required]
        public string Phone { get; set; }
    }
}
