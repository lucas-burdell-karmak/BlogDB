using System;
using System.ComponentModel.DataAnnotations;
using BlogDB.Core;

namespace The_Intern_MVC.Models
{
    public class UserLogin
    {

        [Required]
        [StringLength(64, MinimumLength = 6)]
        public string Username { get; set; }
        
        [Required]
        [StringLength(32, MinimumLength = 6)]
        public string Password { get; set; }
    }
}