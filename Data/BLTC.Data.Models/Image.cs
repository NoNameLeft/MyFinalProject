namespace BLTC.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BLTC.Data.Common.Models;

    public class Image : BaseDeletableModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Manufacturers = new HashSet<Manufacturer>();
        }

        public int? ItemId { get; set; }

        public virtual Item Item { get; set; }

        public int? ArticleId { get; set; }

        public virtual Article Article { get; set; }

        public int? AuthorId { get; set; }

        public Author Author { get; set; }

        [Required]
        public string AddedByEmployeeId { get; set; }

        public virtual ApplicationUser AddedByEmployee { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Extension { get; set; }

        public virtual ICollection<Manufacturer> Manufacturers { get; set; }
    }
}
