namespace BLTC.Web.ViewModels.Orders
{
    using System.ComponentModel.DataAnnotations;

    using BLTC.Web.ViewModels.Common;

    public abstract class BaseOrderInputModel : BindableBase
    {
        private string fullName;
        private string cardName;
        private string email;
        private decimal totalPrice;
        private string token;

        public string Token
        {
            get { return this.token; }
            set { this.SetProperty(ref this.token, value); }
        }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return this.fullName; }
            set { this.SetProperty(ref this.fullName, value); }
        }

        [Required]
        [Display(Name = "Name on Card")]
        public string CardName
        {
            get { return this.cardName; }
            set { this.SetProperty(ref this.cardName, value); }
        }

        [Required]
        public string Email
        {
            get { return this.email; }
            set { this.SetProperty(ref this.email, value); }
        }

        [Required]
        [Display(Name = "Total")]
        public decimal TotalPrice
        {
            get { return this.totalPrice; }
            set { this.SetProperty(ref this.totalPrice, value); }
        }
    }
}
