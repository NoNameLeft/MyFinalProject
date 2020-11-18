namespace BLTC.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BLTC.Data.Common.Models;
    using BLTC.Data.Models.Enums;

    public partial class Order : BaseDeletableModel<int>
    {
        public Order()
        {
            this.Items = new HashSet<OrderItem>();
        }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public DateTime OrderedOn { get; set; } = DateTime.UtcNow;

        [Required]
        public OrderStatus Status { get; set; } = OrderStatus.Checking;

        public virtual ICollection<OrderItem> Items { get; set; }
    }
}
