namespace BLTC.Web.ViewModels.Items
{
    public class ItemsAllFilteredViewModel : ItemsAllViewModel
    {
        private string typeFilter;
        private string shapeFilter;
        private decimal? weightFilter;

        public string TypeFilter
        {
            get { return this.typeFilter; }
            set { this.SetProperty(ref this.typeFilter, value); }
        }

        public string ShapeFilter
        {
            get { return this.shapeFilter; }
            set { this.SetProperty(ref this.shapeFilter, value); }
        }

        public decimal? WeightFilter
        {
            get { return this.weightFilter; }
            set { this.SetProperty(ref this.weightFilter, value); }
        }
    }
}
