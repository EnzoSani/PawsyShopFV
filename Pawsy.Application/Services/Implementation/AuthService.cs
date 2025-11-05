using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Pawsy.Application.Common.Interfaces;
using Pawsy.Application.Services.Interface;
using Pawsy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawsy.Application.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AuthService> _logger;

        public AuthService(UserManager<ApplicationUser> userManager, ITokenService tokenService, IUnitOfWork unitOfWork, ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<(string accessToken, string refreshToken)> LoginAsync(string email, string password)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null || !await _userManager.CheckPasswordAsync(user, password))
                    throw new UnauthorizedAccessException("Invalid credentials");

                var roles = await _userManager.GetRolesAsync(user);
                var accessToken = await _tokenService.GenerateAccessTokenAsync(user, roles);
                var refreshToken = _tokenService.GenerateRefreshToken(user.Id);

                await _unitOfWork.RefreshTokens.AddAsync(refreshToken);
                await _unitOfWork.SaveAsync();

                _logger.LogInformation("User {Email} logged in successfully", email);
                return (accessToken, refreshToken.Token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login failed for {Email}", email);
                throw;
            }
        }

        public async Task<bool> RegisterAsync(string email, string password, string fullName)
        {
            try
            {
                var user = new ApplicationUser { Email = email, UserName = email, FullName = fullName,
                    Name = fullName
                };
                _logger.LogInformation("Attempting to register user: {Email}, FullName: {FullName}", email, fullName);

                var result = await _userManager.CreateAsync(user, password);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    _logger.LogWarning("Registration failed for {Email}: {Errors}", email, errors);
                    throw new Exception($"Registration failed: {errors}");
                }

                await _userManager.AddToRoleAsync(user, "Customer");
                _logger.LogInformation("User {Email} registered successfully", email);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering user {Email}", email);
                throw;
            }
        }

        public async Task<(string accessToken, string refreshToken)> RefreshAsync(string refreshToken)
        {
            try
            {
                var existing = await _unitOfWork.RefreshTokens.GetByTokenAsync(refreshToken);
                if (existing == null || existing.IsUsed || existing.IsRevoked || existing.ExpiresAt < DateTime.UtcNow)
                    throw new UnauthorizedAccessException("Invalid or expired refresh token");

                existing.IsUsed = true;
                await _unitOfWork.RefreshTokens.UpdateAsync(existing);
                await _unitOfWork.SaveAsync();

                var user = existing.User;
                var roles = await _userManager.GetRolesAsync(user);
                var newAccessToken = await _tokenService.GenerateAccessTokenAsync(user, roles);
                var newRefreshToken = _tokenService.GenerateRefreshToken(user.Id);

                await _unitOfWork.RefreshTokens.AddAsync(newRefreshToken);
                await _unitOfWork.SaveAsync();

                _logger.LogInformation("Refresh token renewed for user {Email}", user.Email);
                return (newAccessToken, newRefreshToken.Token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error refreshing token");
                throw;
            }
        }

    }
}
