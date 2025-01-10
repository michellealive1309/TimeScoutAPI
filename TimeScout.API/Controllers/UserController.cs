using System;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAsync(int id)
        {
            var users = await _userService.GetUserByIdAsync(id);

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
                return NotFound("User not found");
            }

            return Ok("User logged out successfully");
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