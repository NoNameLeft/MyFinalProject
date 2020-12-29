namespace BLTC.Web.ViewModels.Orders
{
    using System.Collections.Generic;

    public class ReceiptsListViewModel
    {
        public ReceiptsListViewModel()
        {
            this.Receipts = new HashSet<ReceiptViewModel>();
        }

        public virtual ICollection<ReceiptViewModel> Receipts { get; set; }
    }
}
