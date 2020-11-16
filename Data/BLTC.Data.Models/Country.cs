namespace BLTC.Data.Models
{
    using System.Collections.Generic;

    using BLTC.Data.Common.Models;

    public class Country : BaseDeletableModel<int>
    {
        public Country()
        {
            this.Manufacturers = new HashSet<Manufacturer>();
        }

        public string Name { get; set; }

        public string IsoCode { get; set; }

        public virtual ICollection<Manufacturer> Manufacturers { get; set; }
    }
}
