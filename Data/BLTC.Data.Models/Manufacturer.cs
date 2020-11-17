namespace BLTC.Data.Models
{
    using System.Collections.Generic;

    using BLTC.Data.Common.Models;

    public class Manufacturer : BaseDeletableModel<int>
    {
        public Manufacturer()
        {
            this.Products = new HashSet<Item>();
        }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public string LogoId { get; set; }

        public virtual Image Logo { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Item> Products { get; set; }
    }
}
