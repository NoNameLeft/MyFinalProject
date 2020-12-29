namespace BLTC.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BLTC.Services.Data.Models;
    using Microsoft.AspNetCore.Http;

    public interface IArticlesService
    {
        Task CreateArticleAsync(CreateArticleDto dto, IFormFile avatar, string imageFolder);

        IEnumerable<T> GetTitles<T>();

        ArticlesDto GetByTitle(string title);
    }
}
