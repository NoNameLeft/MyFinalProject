﻿namespace BLTC.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using BLTC.Data.Common.Models;

    public class OrderItem : BaseDeletableModel<int>
    {
        [Required]
        public int ItemId { get; set; }

        public virtual Item Item { get; set; }

        [Required]
        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        public string ShoppingCartId { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
