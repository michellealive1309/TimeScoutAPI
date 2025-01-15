using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeScout.API.DTOs.User;
using TimeScout.API.Models;
using TimeScout.API.Services;

namespace TimeScout.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public UserController(
            IUserService userService,
            IMapper mapper,
            ILogger<UserController> logger
        )
        {
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserAsync()
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var isDelete = await _userService.DeleteUserAsync(userId);

            if (!isDelete)
            {
                return BadRequest("User deletion failed");
            }

            return Ok("User deleted successfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetUserAsync()
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var users = await _userService.GetUserByIdAsync(userId);

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        [HttpGet("logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var isDelete = await _userService.DeleteRefreshTokenAsync(userId);

            if (!isDelete)
            {
                return BadRequest("User logout failed");
            }

            return Ok("User logged out successfully");
        }

        [HttpGet("recover")]
        public async Task<IActionResult> RecoverUserAsync()
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var isRecover = await _userService.RecoverUserAsync(userId);

            if (!isRecover)
            {
                return BadRequest("User recovery failed");
            }

            return Ok("User recovered successfully");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UserUpdateRequestDto userUpdateRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _mapper.Map<User>(userUpdateRequest);
            var updatedUser = await _userService.UpdateUserAsync(user);
            var UserUpdateResponse = _mapper.Map<UserUpdateResponseDto>(updatedUser);

            return Ok(UserUpdateResponse);
        }
    }
}
