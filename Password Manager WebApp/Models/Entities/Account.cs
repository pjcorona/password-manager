using System.ComponentModel.DataAnnotations;

namespace Password_Manager_WebApp.Models.Entities
{
    public class Account
    {
        
        public int AccountID { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Application Name")]
        public string ApplicationName { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Invalid email address format.")]  
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        public string Username { get; set; }

        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Only numeric characters are allowed.")]
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        public string Password { get; set; }

        public string Status { get; set; }
    }
}