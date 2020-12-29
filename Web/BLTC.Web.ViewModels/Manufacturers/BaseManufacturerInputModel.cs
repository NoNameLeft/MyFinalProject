namespace BLTC.Web.ViewModels.Manufacturers
{
    using System.ComponentModel.DataAnnotations;

    using BLTC.Web.ViewModels.Common;

    public abstract class BaseManufacturerInputModel : BindableBase
    {
        private string country;
        private string isoCode;
        private string name;
        private string overview;

        [Required]
        [MinLength(5, ErrorMessage = "Country's name cannot be less than 5 characters.")]
        public string Country
        {
            get { return this.country; }
            set { this.SetProperty(ref this.country, value); }
        }

        [Required]
        [MinLength(2, ErrorMessage = "Country's iso code cannot be less than 2 characters.")]
        public string IsoCode
        {
            get { return this.isoCode; }
            set { this.SetProperty(ref this.isoCode, value); }
        }

        [Required]
        [MinLength(4, ErrorMessage = "Manufacturer's name cannot be less than 4 characters.")]
        public string Name
        {
            get { return this.name; }
            set { this.SetProperty(ref this.name, value); }
        }

        [Required]
        [MinLength(10, ErrorMessage = "Manufacturer's overview cannot be less than 5 characters.")]
        public string Overview
        {
            get { return this.overview; }
            set { this.SetProperty(ref this.overview, value); }
        }
    }
}
