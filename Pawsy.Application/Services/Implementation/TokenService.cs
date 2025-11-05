using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Pawsy.Application.Services.Interface;
using Pawsy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Pawsy.Application.Services.Implementation
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<TokenService> _logger;

        public TokenService(IConfiguration config, ILogger<TokenService> logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task<string> GenerateAccessTokenAsync(ApplicationUser user, IList<string> roles)
        {
            try
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach (var role in roles)
                    claims.Add(new Claim(ClaimTypes.Role, role));

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(double.Parse(_config["Jwt:AccessTokenExpirationMinutes"]!)),
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating access token");
                throw;
            }
        }

        public RefreshToken GenerateRefreshToken(string userId)
        {
            return new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                UserId = userId,
                ExpiresAt = DateTime.UtcNow.AddDays(
                    Convert.ToDouble(_config["Jwt:RefreshTokenExpirationDays"])
                )
            };
        }
    }
}
