namespace BLTC.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BLTC.Data.Common.Models;

    public class Author : BaseDeletableModel<int>
    {
        public Author()
        {
            this.Articles = new HashSet<Article>();
            this.Images = new HashSet<Image>();
        }

        [Required]
        [MinLength(4, ErrorMessage = "Author's name should be no less than 4 characters.")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Author's name should be no less than 6 characters.")]
        public string LastName { get; set; }

        public string Resume { get; set; }

        public virtual ICollection<Article> Articles { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
