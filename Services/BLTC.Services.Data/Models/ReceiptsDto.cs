namespace BLTC.Services.Data.Models
{
    using System;

    public class ReceiptsDto
    {
        public string OrderNumber { get; set; }

        public DateTime OrderedDate { get; set; }

        public string ItemName { get; set; }

        public int Count { get; set; }

        public decimal Price { get; set; }

        public decimal TotalItemPrice { get; set; }
    }
}
