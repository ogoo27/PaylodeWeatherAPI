using System.ComponentModel.DataAnnotations;

namespace PaylodeWeatherAPI.Credential_Auth
{
    public class UserModel
    {
        public string Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }

    }
}