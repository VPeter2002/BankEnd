// AD41HN_HFT_2022231.Models/AuthDtos.cs
using System.ComponentModel.DataAnnotations;

namespace AD41HN_HFT_2022231.Models
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; } // "Orvos" vagy "Páciens"
    }

    public class LoginDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class AuthResponseDto
    {
        public string Token { get; set; }
        public string Role { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}