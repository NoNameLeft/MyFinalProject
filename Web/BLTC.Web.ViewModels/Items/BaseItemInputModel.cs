namespace BLTC.Web.ViewModels.Items
{
    using System.ComponentModel.DataAnnotations;

    using BLTC.Web.ViewModels.Common;
    using Microsoft.AspNetCore.Http;

    public abstract class BaseItemInputModel : BindableBase
    {
        private string username;
        private string name;
        private int type;
        private int shape;
        private decimal weight;
        private decimal purity;
        private int fineness;
        private int quantity;
        private string dimensions;
        private string description;
        private int manufacturerId;
        private IFormFileCollection files;

        public string Username
        {
            get { return this.username; }
            set { this.SetProperty(ref this.username, value); }
        }

        [Required]
        [MinLength(6)]
        public string Name
        {
            get { return this.name; }
            set { this.SetProperty(ref this.name, value); }
        }

        [Required]
        public virtual int Type
        {
            get { return this.type; }
            set { this.SetProperty(ref this.type, value); }
        }

        [Required]
        public virtual int Shape
        {
            get { return this.shape; }
            set { this.SetProperty(ref this.shape, value); }
        }

        [Required]
        [Range(1, 12500)]
        public decimal Weight
        {
            get { return this.weight; }
            set { this.SetProperty(ref this.weight, value); }
        }

        [Required]
        [Display(Name = "Precious metal content (grams):")]
        public decimal Purity
        {
            get { return this.purity; }
            set { this.SetProperty(ref this.purity, value); }
        }

        [Required]
        public int Fineness
        {
            get { return this.fineness; }
            set { this.SetProperty(ref this.fineness, value); }
        }

        [Required]
        public int Quantity
        {
            get { return this.quantity; }
            set { this.SetProperty(ref this.quantity, value); }
        }

        public string Dimensions
        {
            get { return this.dimensions; }
            set { this.SetProperty(ref this.dimensions, value); }
        }

        [Required]
        [MinLength(50)]
        public string Description
        {
            get { return this.description; }
            set { this.SetProperty(ref this.description, value); }
        }

        public int ManufacturerId
        {
            get { return this.manufacturerId; }
            set { this.SetProperty(ref this.manufacturerId, value); }
        }

        [Required]
        public IFormFileCollection Files
        {
            get { return this.files; }
            set { this.SetProperty(ref this.files, value); }
        }
    }
}
