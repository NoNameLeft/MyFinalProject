namespace BLTC.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BLTC.Data.Common.Models;

    public class Receipt : BaseDeletableModel<int>
    {
        public Receipt()
        {
            this.OrderItems = new HashSet<OrderItem>();
        }

        [Required]
        public string CustomerId { get; set; }

        public virtual ApplicationUser Customer { get; set; }

        [Required]
        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
