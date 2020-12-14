namespace BLTC.Web.ViewModels.Items
{
    using System.Collections.Generic;
    using System.ComponentModel;

    using BLTC.Data.Models;

    public abstract class BaseItemViewModel : BaseItemInputModel
    {
        private int id;
        private string manufacturerName;
        private Image manufacturerLogo;
        private decimal price;
        private ICollection<Image> images;

        public BaseItemViewModel()
        {
            this.Id = this.id;
            this.ManufacturerName = this.manufacturerName;
            this.ManufacturerLogo = this.manufacturerLogo;
            this.Price = this.price;
            this.Images = this.images;
        }

        public int Id
        {
            get { return this.id; }
            set { this.SetProperty(ref this.id, value); }
        }

        public string ManufacturerName
        {
            get { return this.manufacturerName; }
            set { this.SetProperty(ref this.manufacturerName, value); }
        }

        public Image ManufacturerLogo
        {
            get { return this.manufacturerLogo; }
            set { this.SetProperty(ref this.manufacturerLogo, value); }
        }

        public decimal Price
        {
            get { return this.price; }
            set { this.SetProperty(ref this.price, value); }
        }

        public ICollection<Image> Images
        {
            get { return this.images; }
            set { this.SetProperty(ref this.images, value); }
        }
    }
}
