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
        public string Name { get; set; }

        [Required]
        public string IsoCode { get; set; }

        public virtual ICollection<Manufacturer> Manufacturers { get; set; }
    }
}
