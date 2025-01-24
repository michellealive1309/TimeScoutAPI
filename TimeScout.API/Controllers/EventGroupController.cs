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
    }
}
