namespace BLTC.Web.ViewModels.Articles
{
    using System.Collections.Generic;

    public class ArticlesAllListViewModel<T>
    {
        public IEnumerable<T> Articles { get; set; }
    }
}
