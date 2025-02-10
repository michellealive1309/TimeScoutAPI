using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeScout.Application.DTOs.Authentication;
using TimeScout.Application.DTOs.Login;
using TimeScout.Domain.Entities;
using TimeScout.Application.Interfaces;
using TimeScout.Application.Security;
using Microsoft.Extensions.Options;
using TimeScout.Application.Settings;

namespace TimeScout.API.Controllers
{
    [Route("api/")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly JwtSetting _jwtSetting;

        public AuthenticationController(
            IIdentityService identityService,
            IMapper mapper,
            ILogger<AuthenticationController> logger,
            IOptions<JwtSetting> options
        )
        {
            _identityService = identityService;
            _mapper = mapper;
            _logger = logger;
            _jwtSetting = options.Value;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            var user = await _identityService.AuthenticateAsync(loginRequest.Email, loginRequest.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            var refreshToken = JwtHelper.GenerateRefreshToken();
            var LoginResponseDto = new LoginResponseDto
            {
                AccessToken = JwtHelper.GenerateJSONWebToken(_jwtSetting, user.Email, user.Id.ToString(), user.Role),
                UserId = user.Id
            };

            await _identityService.UpdateRefreshTokenAsync(user.Id, refreshToken);

            Response.Cookies.Append("refresh_token", refreshToken, new CookieOptions {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            });

            return Ok(LoginResponseDto);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (refreshToken == null)
            {
                return Unauthorized();
            }

            var user = await _identityService.GetUserByRefreshTokenAsync(refreshToken);
            if (user == null)
            {
                return Unauthorized();
            }

            var newRefreshToken = JwtHelper.GenerateRefreshToken();
            var LoginResponseDto = new LoginResponseDto
            {
                AccessToken = JwtHelper.GenerateJSONWebToken(_jwtSetting, user.Email, user.Id.ToString(), user.Role),
                UserId = user.Id
            };

            await _identityService.UpdateRefreshTokenAsync(user.Id, newRefreshToken);

            Response.Cookies.Append("refresh_token", newRefreshToken, new CookieOptions {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            });

            return Ok(LoginResponseDto);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newUser = _mapper.Map<User>(registerRequest);
            var created = await _identityService.CreateUserAsync(newUser);

            if (!created)
            {
                return BadRequest("User already exists.");
            }

            return Ok();
        }
    }
}
