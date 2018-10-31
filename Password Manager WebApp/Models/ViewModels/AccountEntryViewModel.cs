using Password_Manager_WebApp.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Password_Manager_WebApp.Models.ViewModels
{
    public class AccountEntryViewModel
    {
        public Account AccountDetails { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        [Display(Name = "Repeat Password")]
        public string RepeatPassword { get; set; }
    }
}