namespace BLTC.Services.Data.Models
{
    using System.Collections.Generic;

    public class CartsItemsDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string CartId { get; set; }

        public string Images { get; set; }
    }
}
