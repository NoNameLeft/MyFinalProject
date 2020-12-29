namespace BLTC.Web.ViewModels.Messages
{
    using System.ComponentModel.DataAnnotations;

    using BLTC.Web.ViewModels.Common;

    public abstract class BaseContactMessageInputModel : BindableBase
    {
        private string firstName;
        private string lastName;
        private string email;
        private string subject;

        [Required]
        [MinLength(4, ErrorMessage = "Your name should be no less than 4 characters.")]
        public string FirstName
        {
            get { return this.firstName; }
            set { this.SetProperty(ref this.firstName, value); }
        }

        [Required]
        [MinLength(6, ErrorMessage = "Your last name should be no less than 6 characters.")]
        public string LastName
        {
            get { return this.lastName; }
            set { this.SetProperty(ref this.lastName, value); }
        }

        [Required]
        [EmailAddress(ErrorMessage = "Your email address is invalid")]
        public string Email
        {
            get { return this.email; }
            set { this.SetProperty(ref this.email, value); }
        }

        [Required]
        [MinLength(50, ErrorMessage = "Your message should be at least 50 characters long.")]
        public string Subject
        {
            get { return this.subject; }
            set { this.SetProperty(ref this.subject, value); }
        }
    }
}
