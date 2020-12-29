namespace BLTC.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BLTC.Data.Common.Models;

    public class ShoppingCart : BaseDeletableModel<string>
    {
        public ShoppingCart()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Items = new HashSet<OrderItem>();
        }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<OrderItem> Items { get; set; }
    }
}
