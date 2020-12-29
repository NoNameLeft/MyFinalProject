namespace BLTC.Web.ViewModels.Orders
{
    public class OrderItemViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string ImagePaths { get; set; }

        public string CartId { get; set; }
    }
}
