namespace BLTC.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using BLTC.Data.Common.Models;

    public class ContactMessage : BaseDeletableModel<string>
    {
        public ContactMessage()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MinLength(4, ErrorMessage = "Your name should be no less than 4 characters.")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Your last name should be no less than 6 characters.")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Your email address is invalid")]
        public string Email { get; set; }

        [Required]
        [MinLength(50, ErrorMessage = "Your message should be at least 50 characters long.")]
        public string Subject { get; set; }
    }
}
