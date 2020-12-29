namespace BLTC.Web.ViewModels.Manufacturers
{
    using Microsoft.AspNetCore.Http;

    public class ManufacturersAddInputModel : BaseManufacturerInputModel
    {
        private IFormFile files;

        public IFormFile Files
        {
            get { return this.files; }
            set { this.SetProperty(ref this.files, value); }
        }
    }
}
