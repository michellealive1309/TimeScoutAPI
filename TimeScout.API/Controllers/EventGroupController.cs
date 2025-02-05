using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeScout.API.DTOs.EventGroup;
using TimeScout.API.Models;
using TimeScout.API.Services;

namespace TimeScout.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class EventGroupController : ControllerBase
    {
        private readonly IEventGroupService _eventGroupService;
        private readonly IMapper _mapper;
        private readonly ILogger<EventGroupController> _logger;

        public EventGroupController(
            IEventGroupService eventGroupService,
            IMapper mapper,
            ILogger<EventGroupController> logger
        )
        {
            _eventGroupService = eventGroupService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEventGroupAsync([FromBody] EventGroupCreateRequestDto eventGroupCreateRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newEventGroup = _mapper.Map<EventGroup>(eventGroupCreateRequest);
            var result = await _eventGroupService.CreateEventGroupAsync(newEventGroup);

            if (!result)
            {
                return BadRequest("Event group creation failed.");
            }

            return Ok("Event group creation succees.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventGroupAsync(int id)
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _eventGroupService.DeleteEventGroupAsync(id, userId);

            if (!result)
            {
                return BadRequest("Event group deletion failed.");
            }

            return Ok("Event group deletion succees.");
        }

        [HttpGet("all")]
        public async Task<ActionResult<EventGroupResponseDto>> GetAllEventGroupAsync()
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var eventGroups = await _eventGroupService.GetAllEventGroupAsync(userId);

            return Ok(_mapper.Map<IEnumerable<EventGroupResponseDto>>(eventGroups));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventGroupResponseDto>> GetEventGroupByIdAsync(int id)
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var eventGroup = await _eventGroupService.GetEventGroupByIdAsync(id, userId);

            if (eventGroup == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<EventGroupResponseDto>(eventGroup));
        }

        [HttpPut]
        public async Task<ActionResult<EventGroupResponseDto>> UpdateEventGroupAsync([FromBody] EventGroupUpdateRequestDto eventGroupUpdateRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var updateEventGroup = _mapper.Map<EventGroup>(eventGroupUpdateRequest);
            var updatedEventGroup = await _eventGroupService.UpdateEventGroupAsync(updateEventGroup, userId);

            if (updatedEventGroup == null)
            {
                return BadRequest("Event group updating failed.");
            }

            return Ok(_mapper.Map<EventGroupResponseDto>(updatedEventGroup));
        }
    }
}
