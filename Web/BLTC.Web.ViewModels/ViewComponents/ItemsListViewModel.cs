namespace BLTC.Web.ViewModels.ViewComponents
{
    using System.Collections.Generic;

    public class ItemsListViewModel<T>
    {
        public IEnumerable<T> Items { get; set; }
    }
}
