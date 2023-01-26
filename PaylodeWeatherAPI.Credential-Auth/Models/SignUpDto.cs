using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaylodeWeatherAPI.Credential_Auth.Models
{
    public class SignUpDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [Compare("Password")]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
