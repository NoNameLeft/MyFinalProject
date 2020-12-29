namespace BLTC.Web.ViewModels.ViewComponents
{
    using System.Collections.Generic;

    public class CheckoutItemsListViewModel
    {
        public CheckoutItemsListViewModel()
        {
            this.CartItems = new HashSet<CheckoutItemsViewModel>();
        }

        public virtual ICollection<CheckoutItemsViewModel> CartItems { get; set; }
    }
}
