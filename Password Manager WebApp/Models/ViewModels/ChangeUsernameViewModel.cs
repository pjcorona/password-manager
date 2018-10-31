using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Password_Manager_WebApp.Models.ViewModels
{
    public class ChangeUsernameViewModel
    {
        [Required]
        [MinLength(8, ErrorMessage = "Username should have at least 8 characters.")]
        [Display(Name = "New Username")]
        public string NewUsername { get; set; }
    }
}