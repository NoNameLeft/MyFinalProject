﻿namespace BLTC.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BLTC.Data.Common.Models;

    public class Author : BaseDeletableModel<int>
    {
        public Author()
        {
            this.Articles = new HashSet<AuthorArticle>();
        }

        [Required]
        public string AvatarId { get; set; }

        public virtual Image Avatar { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Resume { get; set; }

        public virtual ICollection<AuthorArticle> Articles { get; set; }
    }
}
