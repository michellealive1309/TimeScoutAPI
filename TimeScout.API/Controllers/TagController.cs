using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeScout.API.DTOs.Event;
using TimeScout.API.Models;
using TimeScout.API.Services;

namespace TimeScout.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;
        private readonly ILogger<TagController> _logger;

        public TagController(ITagService tagService, IMapper mapper, ILogger<TagController> logger)
        {
            _tagService = tagService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTagByIdAsync(int id)
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var tag = await _tagService.GetTagByIdAsync(id, userId);

            if (tag == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TagResponseDto>(tag));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTagsAsync()
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var tags = await _tagService.GetAllTagsAsync(userId);

            return Ok(_mapper.Map<IEnumerable<TagResponseDto>>(tags));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTagAsync([FromBody] TagRequestDto tagRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tag = _mapper.Map<Tag>(tagRequest);
            var newTag = await _tagService.CreateTagAsync(tag);

            if (newTag == null)
            {
                return BadRequest("Tag creation failed.");
            }

            return Ok(tag.Id);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTagAsync([FromBody] TagRequestDto tagRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tag = _mapper.Map<Tag>(tagRequest);
            var result = await _tagService.UpdateTagAsync(tag);

            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTagAsync(int id)
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _tagService.DeleteTagAsync(id, userId);

            if (!result)
            {
                return BadRequest("Tag deletion failed.");
            }

            return NoContent();
        }
    }
}
