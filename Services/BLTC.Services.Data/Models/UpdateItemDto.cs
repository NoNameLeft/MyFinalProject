namespace BLTC.Services.Data.Models
{
    using Microsoft.AspNetCore.Http;

    public class UpdateItemDto
    {
        public string Name { get; set; }

        public virtual int Type { get; set; }

        public virtual int Shape { get; set; }

        public decimal Weight { get; set; }

        public decimal Purity { get; set; }

        public int Fineness { get; set; }

        public int Quantity { get; set; }

        public string Dimensions { get; set; }

        public string Description { get; set; }

        public int ManufacturerId { get; set; }

        public int ItemId { get; set; }

        public IFormFileCollection Images { get; set; }
    }
}
