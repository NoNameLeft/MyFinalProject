namespace BLTC.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using BLTC.Data.Common.Models;

    public class AuthorArticle : BaseDeletableModel<int>
    {
        [Required]
        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }

        [Required]
        public int ArticleId { get; set; }

        public virtual Article Article { get; set; }
    }
}
