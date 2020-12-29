namespace BLTC.Web.Controllers
{
    using System.IO;
    using System.Threading.Tasks;

    using BLTC.Services.Data;
    using BLTC.Services.Data.Models;
    using BLTC.Web.ViewModels.Articles;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    public class ArticlesController : BaseController
    {
        private readonly IArticlesService articlesService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IUsersService usersService;

        public ArticlesController(
            IArticlesService articlesService,
            IWebHostEnvironment webHostEnvironment,
            IUsersService usersService)
        {
            this.articlesService = articlesService;
            this.webHostEnvironment = webHostEnvironment;
            this.usersService = usersService;
        }

        [Authorize]
        public IActionResult Write()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Write(ArticleWriteInputModel input)
        {
            var dto = new CreateArticleDto
            {
                Title = input.Title,
                Text = input.Text,
                FirstName = input.FirstName,
                LastName = input.LastName,
                Resume = input.Resume,
                UserId = await this.usersService.GetIdAsync(this.User.Identity.Name),
            };

            await this.articlesService.CreateArticleAsync(dto, input.Files, Path.Combine(this.webHostEnvironment.WebRootPath, "images", "authors"));
            return this.Redirect("/");
        }

        [AllowAnonymous]
        public IActionResult GetByTitle(string title)
        {
            var article = this.articlesService.GetByTitle(title);
            var viewModel = new ArticleSingleViewModel
            {
                Title = article.Title,
                Text = article.Text,
                AuthorsFirstName = article.AuthorsFirstName,
                AuthorsLastName = article.AuthorsLastName,
                AuthorsResume = article.AuthorsResume,
                AuthorsAvatar = article.AuthorsAvatar,
            };

            return this.View(viewModel);
        }
    }
}
