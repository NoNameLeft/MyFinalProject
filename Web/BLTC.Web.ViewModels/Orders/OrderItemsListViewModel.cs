namespace BLTC.Web.ViewModels.Orders
{
    using System.Collections.Generic;

    public class OrderItemsListViewModel
    {
        public OrderItemsListViewModel()
        {
            this.CartItems = new HashSet<OrderItemViewModel>();
        }

        public virtual ICollection<OrderItemViewModel> CartItems { get; set; }
    }
}
