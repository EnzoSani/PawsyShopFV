using System.ComponentModel.DataAnnotations;

namespace PawsyShop.Api.Models.Auth
{
    public class RegisterUserRequest
    {
        [Required, EmailAddress]
        public string Email { get; init; } = string.Empty;

        [Required, MinLength(6)]
        public string Password { get; init; } = string.Empty;

        [Required]
        public string FullName { get; init; } = string.Empty;
    }
}
