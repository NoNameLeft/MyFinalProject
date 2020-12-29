namespace BLTC.Web.ViewModels.Articles
{
    using BLTC.Data.Models;

    public abstract class BaseArticleViewModel
    {
        private string title;
        private string text;
        private string authorsFirstname;
        private string authorsLastname;
        private string authorsResume;
        private string authorsAvatar;

        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        public string Text
        {
            get { return this.text; }
            set { this.text = value; }
        }

        public string AuthorsFirstName
        {
            get { return this.authorsFirstname; }
            set { this.authorsFirstname = value; }
        }

        public string AuthorsLastName
        {
            get { return this.authorsLastname; }
            set { this.authorsLastname = value; }
        }

        public string AuthorsResume
        {
            get { return this.authorsResume; }
            set { this.authorsResume = value; }
        }

        public string AuthorsAvatar
        {
            get { return this.authorsAvatar; }
            set { this.authorsAvatar = value; }
        }
    }
}
