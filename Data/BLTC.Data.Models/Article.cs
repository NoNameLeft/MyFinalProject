namespace BLTC.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BLTC.Data.Common.Models;

    public class Article : BaseDeletableModel<int>
    {
        public Article()
        {
            this.Images = new HashSet<Image>();
            this.Authors = new HashSet<AuthorArticle>();
        }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }

        public int Votes { get; set; } = 0;

        public int Views { get; set; } = 0;

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }

        public virtual ICollection<AuthorArticle> Authors { get; set; }
    }
}
