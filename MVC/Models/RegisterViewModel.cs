using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace The_Intern_MVC.Models
{
    public class RegisterViewModel
    {

        [Required]
        [StringLength(64, MinimumLength = 6)]
        public string Username { get; set; }
        
        [Required]
        [StringLength(32, MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}