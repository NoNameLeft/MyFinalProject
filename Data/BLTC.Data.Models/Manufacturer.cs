namespace BLTC.Data.Models
{
    using System.Collections.Generic;

    using BLTC.Data.Common.Models;

    public class Manufacturer : BaseDeletableModel<int>
    {
        public Manufacturer()
        {
            this.Items = new HashSet<Item>();
        }

        public virtual Country Country { get; set; }

        public string ImageId { get; set; }

        public Image Image { get; set; }

        public string Name { get; set; }

        public int CountryId { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
