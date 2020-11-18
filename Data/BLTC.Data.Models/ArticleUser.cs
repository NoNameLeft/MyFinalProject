namespace BLTC.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using BLTC.Data.Common.Models;

    public class ArticleUser : BaseDeletableModel<int>
    {
        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public int ArticleId { get; set; }

        public virtual Article Article { get; set; }
    }
}
