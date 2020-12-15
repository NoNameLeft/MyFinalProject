namespace BLTC.Web.ViewModels.Countries
{
    using BLTC.Web.ViewModels.Common;

    public abstract class BaseCountryInputModel : BindableBase
    {
        private string name;
        private string isoCode;

        public BaseCountryInputModel()
        {
            this.IsoCode = this.isoCode;
            this.Name = this.Name;
        }

        public string Name
        {
            get { return this.name; }
            set { this.SetProperty(ref this.name, value); }
        }

        public string IsoCode
        {
            get { return this.isoCode; }
            set { this.SetProperty(ref this.isoCode, value); }
        }
    }
}
