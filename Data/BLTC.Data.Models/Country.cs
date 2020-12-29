namespace BLTC.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BLTC.Data.Common.Models;

    public class Country : BaseDeletableModel<int>
    {
        public Country()
        {
            this.Manufacturers = new HashSet<Manufacturer>();
        }

        [Required]
        [MinLength(5, ErrorMessage = "Name cannot be less than 5 characters.")]
        public string Name { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Iso code cannot be less than 2 characters.")]
        public string IsoCode { get; set; }

        public virtual ICollection<Manufacturer> Manufacturers { get; set; }
    }
}
