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
    }
}
