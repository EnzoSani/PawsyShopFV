using System.ComponentModel.DataAnnotations;

namespace PawsyShop.Api.Models.Auth
{
    public class RefreshUserRequest
    {
        [Required]
        public string RefreshToken { get; init; } = string.Empty;
    }
}
