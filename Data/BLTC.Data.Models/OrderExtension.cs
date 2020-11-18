namespace BLTC.Data.Models
{
    using System.Linq;

    public partial class Order
    {
        public decimal TotalPrice
        {
            get { return this.Items.Sum(x => x.Item.Price); }
        }
    }
}
