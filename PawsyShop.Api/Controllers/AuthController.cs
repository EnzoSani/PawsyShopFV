using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Pawsy.Application.Services.Interface;
using PawsyShop.Api.Models.Auth;
using System.Net;

namespace PawsyShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            _logger.LogInformation("Incoming register request: {Email} - {FullName}", request.Email, request.FullName);
            try
            {
                var result = await _authService.RegisterAsync(request.Email, request.Password, request.FullName);
                if (!result)
                    return BadRequest("Registration failed.");

                return Ok("User registered successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering user");
                return StatusCode((int)HttpStatusCode.InternalServerError, "Internal server error");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
        {
            try
            {
                var (access, refresh) = await _authService.LoginAsync(request.Email, request.Password);
                return Ok(new { accessToken = access, refreshToken = refresh });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid credentials.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error logging in user");
                return StatusCode((int)HttpStatusCode.InternalServerError, "Internal server error");
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
        {
            try
            {
                var (access, refresh) = await _authService.RefreshAsync(request.RefreshToken);
                return Ok(new { accessToken = access, refreshToken = refresh });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid or expired token.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error refreshing token");
                return StatusCode((int)HttpStatusCode.InternalServerError, "Internal server error");
            }
        }
    }
}
