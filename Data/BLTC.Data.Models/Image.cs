namespace BLTC.Data.Models
{
    using System;
    using System.Collections.Generic;

    using BLTC.Data.Common.Models;

    public class Image : BaseModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Manufacturers = new HashSet<Manufacturer>();
        }

        public int ItemId { get; set; }

        public virtual Item Item { get; set; }

        public string Name { get; set; }

        public string Extension { get; set; }

        public ICollection<Manufacturer> Manufacturers { get; set; }
    }
}
