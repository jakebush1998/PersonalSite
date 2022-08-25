using System.ComponentModel.DataAnnotations;

namespace PersonalSite.UI.MVC.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "* Must provide a name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "* Must provide an email address")]
        [DataType(DataType.EmailAddress, ErrorMessage = "* Email address must be formatted correctly (you@domain.com)")]
        public string Email { get; set; }

        [Required(ErrorMessage ="* Must provide a subject")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "* Must provide a message")]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
    }
}
