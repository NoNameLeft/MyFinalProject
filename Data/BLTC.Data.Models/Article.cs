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
        }

        [Required]
        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Article's name should be no less than 10 characters.")]
        public string Title { get; set; }

        [Required]
        [MinLength(80, ErrorMessage = "Article's text should be no less than 80 characters.")]
        public string Text { get; set; }

        public int Votes { get; set; } = 0;

        public int Views { get; set; } = 0;

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
