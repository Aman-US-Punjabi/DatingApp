using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 8, ErrorMessage = "You must specify password between 8 and 25 characters.")]
        public string Password { get; set; }
    }
}