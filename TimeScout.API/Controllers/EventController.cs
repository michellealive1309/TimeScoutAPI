using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TimeScout.API.DTOs.Event;
using TimeScout.API.Models;
using TimeScout.API.Services;

namespace TimeScout.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;
        private readonly ILogger<EventController> _logger;

        public EventController(
            IEventService eventService,
            IMapper mapper,
            ILogger<EventController> logger
        )
        {
            _eventService = eventService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventAsync(int id)
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (!await _eventService.DeleteEventAsync(id, userId))
            {
                return BadRequest("Event deletion failed.");
            }

            return Ok("Event deletion success.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventAsync(int id)
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _eventService.GetEventByIdAsync(id, userId);

            if (result == null)
            {
                return NotFound();
            }

            var responseDto = _mapper.Map<EventResponseDto>(result);

            return Ok(responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEventAsync([FromBody] EventCreateRequestDto eventCreateRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newEvent = _mapper.Map<Event>(eventCreateRequest);

            try
            {
                var result = await _eventService.CreateEventAsync(newEvent);

                if (!result)
                {
                    return BadRequest("Event creation failed.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok("Event creation success.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEventAsync([FromBody] EventUpdateRequestDto eventUpdateRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updateEvent = _mapper.Map<Event>(eventUpdateRequest);
            var result = await _eventService.UpdateEventAsync(updateEvent);

            if (result == null)
            {
                return BadRequest("Event updating failed.");
            }

            var responseDto = _mapper.Map<EventResponseDto>(updateEvent);

            return Ok(responseDto);
        }
    }
}
