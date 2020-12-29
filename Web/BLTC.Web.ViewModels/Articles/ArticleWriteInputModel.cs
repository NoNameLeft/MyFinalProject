namespace BLTC.Web.ViewModels.Articles
{
    using Microsoft.AspNetCore.Http;

    public class ArticleWriteInputModel : BaseArticleInputModel
    {
        private IFormFile files;

        public IFormFile Files
        {
            get { return this.files; }
            set { this.SetProperty(ref this.files, value); }
        }
    }
}
