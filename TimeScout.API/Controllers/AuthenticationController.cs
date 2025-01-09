using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeScout.API.DTOs.Authentication;
using TimeScout.API.DTOs.Login;
using TimeScout.API.Services;

namespace TimeScout.API.Controllers
{
    [Route("api/")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(
            IIdentityService identityService,
            ILogger<AuthenticationController> logger
        )
        {
            _identityService = identityService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            var user = await _identityService.AuthenticateAsync(loginRequest.Email, loginRequest.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            var accessToken = _identityService.GenerateJSONWebToken(user.Email, user.Id.ToString(), user.Role);
            var refreshToken = _identityService.GenerateRefreshToken();
            var LoginResponseDto = new LoginResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            await _identityService.UpdateRefreshTokenAsync(user.Id, refreshToken);

            return Ok(LoginResponseDto);
        }
    }
}
