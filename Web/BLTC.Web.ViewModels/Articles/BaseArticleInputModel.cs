namespace BLTC.Web.ViewModels.Articles
{
    using System.ComponentModel.DataAnnotations;

    using BLTC.Web.ViewModels.Common;

    public abstract class BaseArticleInputModel : BindableBase
    {
        private string title;
        private string text;
        private string firstname;
        private string lastname;
        private string resume;

        [Required]
        [MinLength(10, ErrorMessage = "Title should be no less than 10 characters.")]
        public string Title
        {
            get { return this.title; }
            set { this.SetProperty(ref this.title, value); }
        }

        [Required]
        [MinLength(80, ErrorMessage = "Text should be no less than 80 characters.")]
        public string Text
        {
            get { return this.text; }
            set { this.SetProperty(ref this.text, value); }
        }

        [Required]
        [MinLength(4, ErrorMessage = "Author's name should be no less than 4 characters.")]
        public string FirstName
        {
            get { return this.firstname; }
            set { this.SetProperty(ref this.firstname, value); }
        }

        [Required]
        [MinLength(4, ErrorMessage = "Author's name should be no less than 4 characters.")]
        public string LastName
        {
            get { return this.lastname; }
            set { this.SetProperty(ref this.lastname, value); }
        }

        public string Resume
        {
            get { return this.resume; }
            set { this.SetProperty(ref this.resume, value); }
        }
    }
}
