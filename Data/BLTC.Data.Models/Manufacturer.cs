namespace BLTC.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BLTC.Data.Common.Models;

    public class Manufacturer : BaseDeletableModel<int>
    {
        public Manufacturer()
        {
            this.Products = new HashSet<Item>();
        }

        [Required]
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        [Required]
        public string LogoId { get; set; }

        public virtual Image Logo { get; set; }

        [Required]
        [MinLength(4)]
        public string Name { get; set; }

        [Required]
        public string Overview { get; set; }

        public virtual ICollection<Item> Products { get; set; }
    }
}
