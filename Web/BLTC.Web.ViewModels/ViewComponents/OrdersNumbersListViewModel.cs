namespace BLTC.Web.ViewModels.ViewComponents
{
    using System.Collections.Generic;

    public class OrdersNumbersListViewModel
    {
        public OrdersNumbersListViewModel()
        {
            this.Orders = new HashSet<OrdersNumbersViewModel>();
        }

        public virtual ICollection<OrdersNumbersViewModel> Orders { get; set; }
    }
}
