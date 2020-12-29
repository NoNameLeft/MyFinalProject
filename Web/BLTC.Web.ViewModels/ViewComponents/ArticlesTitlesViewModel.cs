namespace BLTC.Web.ViewModels.ViewComponents
{
    using BLTC.Data.Models;
    using BLTC.Services.Mapping;

    public class ArticlesTitlesViewModel : IMapFrom<Article>
    {
        public string Title { get; set; }
    }
}
