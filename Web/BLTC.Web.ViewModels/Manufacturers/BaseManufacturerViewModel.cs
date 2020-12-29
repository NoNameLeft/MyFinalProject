namespace BLTC.Web.ViewModels.Manufacturers
{
    using BLTC.Data.Models;

    public abstract class BaseManufacturerViewModel : BaseManufacturerInputModel
    {
        private int id;
        private int countryId;
        private Image logo;

        public int Id
        {
            get { return this.id; }
            set { this.SetProperty(ref this.id, value); }
        }

        public int CountryId
        {
            get { return this.countryId; }
            set { this.SetProperty(ref this.countryId, value); }
        }

        public Image Logo
        {
            get { return this.logo; }
            set { this.SetProperty(ref this.logo, value); }
        }
    }
}
