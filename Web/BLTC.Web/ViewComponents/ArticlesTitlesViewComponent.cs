namespace BLTC.Web.ViewComponents
{
    using BLTC.Services.Data;
    using BLTC.Web.ViewModels.ViewComponents;
    using Microsoft.AspNetCore.Mvc;

    public class ArticlesTitlesViewComponent : ViewComponent
    {
        private readonly IArticlesService articlesService;

        public ArticlesTitlesViewComponent(IArticlesService articlesService)
        {
            this.articlesService = articlesService;
        }

        public IViewComponentResult Invoke()
        {
            var titles = this.articlesService.GetTitles<ArticlesTitlesViewModel>();
            var viewModel = new ItemsListViewModel<ArticlesTitlesViewModel>() { Items = titles };
            return this.View(viewModel);
        }
    }
}
