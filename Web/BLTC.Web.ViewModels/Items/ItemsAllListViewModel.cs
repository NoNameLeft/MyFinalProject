namespace BLTC.Web.ViewModels.Items
{
    using System.Collections.Generic;

    public class ItemsAllListViewModel<T>
    {
        public IEnumerable<T> Items { get; set; }
    }
}
