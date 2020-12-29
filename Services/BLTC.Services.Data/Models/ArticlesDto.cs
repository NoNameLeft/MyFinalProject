namespace BLTC.Services.Data.Models
{
    using BLTC.Data.Models;

    public class ArticlesDto
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public string AuthorsFirstName { get; set; }

        public string AuthorsLastName { get; set; }

        public string AuthorsResume { get; set; }

        public string AuthorsAvatar { get; set; }
    }
}
