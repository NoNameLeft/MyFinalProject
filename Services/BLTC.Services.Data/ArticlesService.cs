namespace BLTC.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using BLTC.Data.Common.Repositories;
    using BLTC.Data.Models;
    using BLTC.Services.Data.Models;
    using BLTC.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class ArticlesService : IArticlesService
    {
        private readonly IDeletableEntityRepository<Article> articlesRepository;
        private readonly IDeletableEntityRepository<Author> authorsRepository;
        private readonly IDeletableEntityRepository<Image> imagesRepository;

        public ArticlesService(
            IDeletableEntityRepository<Article> articlesRepository,
            IDeletableEntityRepository<Author> authorsRepository,
            IDeletableEntityRepository<Image> imagesRepository)
        {
            this.articlesRepository = articlesRepository;
            this.authorsRepository = authorsRepository;
            this.imagesRepository = imagesRepository;
        }

        public async Task CreateArticleAsync(CreateArticleDto dto, IFormFile avatar, string imageFolder)
        {
            if (!this.articlesRepository.All().Any(x => x.Title == dto.Title))
            {
                var author = this.authorsRepository.All().Where(x => x.FirstName == dto.FirstName && x.LastName == dto.LastName).FirstOrDefault();
                if (author is null)
                {
                    author = new Author
                    {
                        FirstName = dto.FirstName,
                        LastName = dto.LastName,
                        Resume = dto.Resume,
                    };

                    author.Images.Add(new Image
                    {
                        AddedByEmployeeId = dto.UserId,
                        Name = avatar.FileName,
                        Extension = this.ChangeToContentType(Path.GetExtension(avatar.FileName).Replace(".", string.Empty).Trim()),
                    });

                    await this.UploadImagesToDirectory(imageFolder, avatar);
                    await this.authorsRepository.AddAsync(author);
                    await this.authorsRepository.SaveChangesAsync();
                }

                var article = new Article
                {
                    Title = dto.Title,
                    Text = dto.Text,
                    AuthorId = author.Id,
                };

                await this.articlesRepository.AddAsync(article);
                await this.articlesRepository.SaveChangesAsync();
            }
        }

        public IEnumerable<T> GetTitles<T>()
        {
            return this.articlesRepository.All().To<T>().AsEnumerable();
        }

        public ArticlesDto GetByTitle(string title)
        {
            var query = this.articlesRepository.AllAsNoTracking()
                .Join(this.authorsRepository.AllAsNoTracking(), a => a.AuthorId, au => au.Id, (a, au) => new { Article = a, Author = au })
                .Join(this.imagesRepository.AllAsNoTracking(), au => au.Author.Id, i => i.AuthorId, (au, i) => new { Author = au, Image = i })
                .Where(x => x.Author.Article.Title == title)
                .Select(a => new
                {
                    a.Author.Article.Title,
                    a.Author.Article.Text,
                    a.Author.Author.FirstName,
                    a.Author.Author.LastName,
                    a.Author.Author.Resume,
                    a.Image.Name,
                }).FirstOrDefault();

            if (query is null)
            {
                throw new ArgumentException($"Article with ${title} doesn't exist.");
            }

            return new ArticlesDto
            {
                Title = query.Title,
                Text = query.Text,
                AuthorsFirstName = query.FirstName,
                AuthorsLastName = query.LastName,
                AuthorsResume = query.Resume,
                AuthorsAvatar = query.Name,
            };
        }

        private async Task UploadImagesToDirectory(string imagesFolder, IFormFile image)
        {
            Directory.CreateDirectory($"{imagesFolder}");
            if (!this.ExistingFileNames(imagesFolder).Any(x => x == image.FileName))
            {
                var path = Path.Combine(imagesFolder, image.FileName);
                await image.CopyToAsync(new FileStream(path, FileMode.Create));
            }
        }

        private string[] ExistingFileNames(string folder)
        {
            return Directory.GetFiles(folder)
                            .Select(Path.GetFileName)
                            .ToArray();
        }

        private string ChangeToContentType(string extention)
        {
            switch (extention)
            {
                case "jpg":
                    extention = "image/jpeg";
                    break;
                case "png":
                    extention = "image/png";
                    break;
                case "gif":
                    extention = "image/gif";
                    break;
                case "svg":
                    extention = "image/svg+xml";
                    break;
                case "tiff":
                    extention = "image/tiff";
                    break;
                case "webp":
                    extention = "image/webp";
                    break;
                case "bmp":
                    extention = "image/bmp";
                    break;
            }

            return extention;
        }
    }
}
