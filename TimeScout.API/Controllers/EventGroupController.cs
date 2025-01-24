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

        [HttpGet("all")]
        public async Task<ActionResult<EventGroupResponseDto>> GetAllEventGroupByIdAsync()
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var eventGroups = await _eventGroupService.GetAllEventGroupAsync(userId);

            return Ok(_mapper.Map<IEnumerable<EventGroupResponseDto>>(eventGroups));
        }
    }
}
