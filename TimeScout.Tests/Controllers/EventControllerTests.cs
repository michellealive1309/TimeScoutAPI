using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TimeScout.API.Controllers;
using TimeScout.API.DTOs.Event;
using TimeScout.API.Models;
using TimeScout.API.Services;

namespace TimeScout.Tests.Controllers
{
    public class EventControllerTests
    {
        [Fact]
        public async Task Test_Delete_Event_Should_Return_BadRequest_Result()
        {
            // Arrange
            var eventServiceMock = new Mock<IEventService>();

            eventServiceMock.Setup(x => x.DeleteEventAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(false);

            var autoMapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger<EventController>>();
            var controllerContext = new ControllerContext() {
                HttpContext = new DefaultHttpContext() {
                    User = new ClaimsPrincipal(new ClaimsIdentity([
                        new Claim(ClaimTypes.NameIdentifier, "1")
                    ]))
                }
            };
            var eventController = new EventController(
                eventServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            ) {
                ControllerContext = controllerContext
            };

            // Act
            var actual = await eventController.DeleteEventAsync(1);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actual);
        }

        [Fact]
        public async Task Test_Delete_Event_Should_Return_Ok_Result()
        {
            // Arrange
            var eventServiceMock = new Mock<IEventService>();

            eventServiceMock.Setup(x => x.DeleteEventAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(true);

            var autoMapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger<EventController>>();
            var controllerContext = new ControllerContext() {
                HttpContext = new DefaultHttpContext() {
                    User = new ClaimsPrincipal(new ClaimsIdentity([
                        new Claim(ClaimTypes.NameIdentifier, "1")
                    ]))
                }
            };
            var eventController = new EventController(
                eventServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            ) {
                ControllerContext = controllerContext
            };

            // Act
            var actual = await eventController.DeleteEventAsync(1);

            // Assert
            Assert.IsType<OkObjectResult>(actual);
        }

        [Theory]
        [InlineData("")]
        [InlineData("lorem")]
        [InlineData(null)]
        public async Task Test_Get_All_Event_Should_Return_BadRequest_Result(string span)
        {
            // Arrange
            var now = DateTime.UtcNow;
            var eventServiceMock = new Mock<IEventService>();
            var autoMapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger<EventController>>();
            var eventController = new EventController(
                eventServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            );

            // Act
            var actual = await eventController.GetAllEventsAsync(span, now);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actual.Result);
        }

        [Theory]
        [InlineData("day")]
        [InlineData("week")]
        [InlineData("biweek")]
        [InlineData("month")]
        [InlineData("year")]
        public async Task Test_Get_All_Event_Should_Return_OkResult_Result(string span)
        {
            // Arrange
            var now = DateTime.UtcNow;
            var events = new List<Event> {
                new Event {
                    Name = "Test",
                    StartDate = DateOnly.FromDateTime(DateTime.UtcNow),
                    StartTime = TimeOnly.FromDateTime(DateTime.UtcNow)
                }
            };
            var eventServiceMock = new Mock<IEventService>();

            eventServiceMock.Setup(x => x.GetAllEventsAsync(span, now, It.IsAny<int>())).ReturnsAsync(events);

            var autoMapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger<EventController>>();
            var controllerContext = new ControllerContext() {
                HttpContext = new DefaultHttpContext() {
                    User = new ClaimsPrincipal(new ClaimsIdentity([
                        new Claim(ClaimTypes.NameIdentifier, "1")
                    ]))
                }
            };
            var eventController = new EventController(
                eventServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            ) {
                ControllerContext = controllerContext
            };

            // Act
            var actual = await eventController.GetAllEventsAsync(span, now);

            // Assert
            Assert.IsType<OkObjectResult>(actual.Result);
        }

        [Fact]
        public async Task Test_Get_Event_Should_Return_NotFound_Result()
        {
            // Arrange
            var eventServiceMock = new Mock<IEventService>();

            eventServiceMock.Setup(x => x.GetEventByIdAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync((Event)null);

            var autoMapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger<EventController>>();
            var controllerContext = new ControllerContext() {
                HttpContext = new DefaultHttpContext() {
                    User = new ClaimsPrincipal(new ClaimsIdentity([
                        new Claim(ClaimTypes.NameIdentifier, "1")
                    ]))
                }
            };
            var eventController = new EventController(
                eventServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            ) {
                ControllerContext = controllerContext
            };

            // Act
            var actual = await eventController.GetEventAsync(1);

            // Assert
            Assert.IsType<NotFoundResult>(actual.Result);
        }

        [Fact]
        public async Task Test_Get_Event_Should_Return_EventResponseDto()
        {
            // Arrange
            var @event = new Event {
                Id = 1,
                Name = "Test",
                UserId = 1
            };
            var eventResponseDto = new EventResponseDto {
                Id = 1,
                Name = "Test",
                UserId = 1
            };
            var eventServiceMock = new Mock<IEventService>();

            eventServiceMock.Setup(x => x.GetEventByIdAsync(1, 1)).ReturnsAsync(@event);

            var autoMapperMock = new Mock<IMapper>();

            autoMapperMock.Setup(x => x.Map<EventResponseDto>(@event)).Returns(eventResponseDto);

            var loggerMock = new Mock<ILogger<EventController>>();
            var controllerContext = new ControllerContext() {
                HttpContext = new DefaultHttpContext() {
                    User = new ClaimsPrincipal(new ClaimsIdentity([
                        new Claim(ClaimTypes.NameIdentifier, "1")
                    ]))
                }
            };
            var eventController = new EventController(
                eventServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            ) {
                ControllerContext = controllerContext
            };

            // Act
            var actual = await eventController.GetEventAsync(1);

            // Assert
            Assert.Same(eventResponseDto, (actual.Result as OkObjectResult).Value);
        }

        [Fact]
        public async Task Test_Create_Event_Model_Invalid_Should_Return_BadRequest_Result()
        {
            // Arrange
            var eventCreateRequestDto = new EventCreateRequestDto {
                Name = ""
            };
            var eventServiceMock = new Mock<IEventService>();
            var autoMapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger<EventController>>();
            var eventController = new EventController(
                eventServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            );

            eventController.ModelState.AddModelError("Name", "Name is required");

            // Act
            var actual = await eventController.CreateEventAsync(eventCreateRequestDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actual);
        }

        [Fact]
        public async Task Test_Create_Event_Should_Return_BadRequest_Result()
        {
            // Arrange
            var eventCreateRequestDto = new EventCreateRequestDto {
                Name = "Test"
            };
            var @event = new Event {
                Name = "Test"
            };
            var eventServiceMock = new Mock<IEventService>();

            eventServiceMock.Setup(x => x.CreateEventAsync(@event)).ReturnsAsync(false);

            var autoMapperMock = new Mock<IMapper>();

            autoMapperMock.Setup(x => x.Map<Event>(eventCreateRequestDto)).Returns(@event);

            var loggerMock = new Mock<ILogger<EventController>>();
            var eventController = new EventController(
                eventServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            );

            // Act
            var actual = await eventController.CreateEventAsync(eventCreateRequestDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actual);
        }

        [Fact]
        public async Task Test_Create_Event_Catch_Exception_Should_Return_BadRequest_Result()
        {
            // Arrange
            var eventCreateRequestDto = new EventCreateRequestDto {
                Name = "Test"
            };
            var @event = new Event {
                Name = "Test"
            };
            var eventServiceMock = new Mock<IEventService>();

            eventServiceMock.Setup(x => x.CreateEventAsync(@event)).Throws<Exception>();

            var autoMapperMock = new Mock<IMapper>();

            autoMapperMock.Setup(x => x.Map<Event>(eventCreateRequestDto)).Returns(@event);

            var loggerMock = new Mock<ILogger<EventController>>();
            var eventController = new EventController(
                eventServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            );

            // Act
            var actual = await eventController.CreateEventAsync(eventCreateRequestDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actual);
        }

        [Fact]        
        public async Task Test_Create_Event_Should_Return_Ok_Result()
        {
            // Arrange
            var eventCreateRequestDto = new EventCreateRequestDto {
                Name = "Test"
            };
            var @event = new Event {
                Name = "Test"
            };
            var eventServiceMock = new Mock<IEventService>();

            eventServiceMock.Setup(x => x.CreateEventAsync(@event)).ReturnsAsync(true);

            var autoMapperMock = new Mock<IMapper>();

            autoMapperMock.Setup(x => x.Map<Event>(eventCreateRequestDto)).Returns(@event);

            var loggerMock = new Mock<ILogger<EventController>>();
            var eventController = new EventController(
                eventServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            );

            // Act
            var actual = await eventController.CreateEventAsync(eventCreateRequestDto);

            // Assert
            Assert.IsType<OkObjectResult>(actual);
        }

        [Fact]
        public async Task Test_Update_Event_Model_Invalid_Should_Return_BadRequest_Result()
        {
            // Arrange
            var eventUpdateRequestDto = new EventUpdateRequestDto {
                Name = ""
            };
            var eventServiceMock = new Mock<IEventService>();
            var autoMapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger<EventController>>();
            var eventController = new EventController(
                eventServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            );

            eventController.ModelState.AddModelError("Name", "Name is required");

            // Act
            var actual = await eventController.UpdateEventAsync(eventUpdateRequestDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actual.Result);
        }

        [Fact]
        public async Task Test_Update_Event_Should_Return_BadRequest_Result()
        {
            // Arrange
            var eventUpdateRequestDto = new EventUpdateRequestDto {
                Name = "Test"
            };
            var @event = new Event {
                Name = "Test"
            };
            var eventServiceMock = new Mock<IEventService>();

            eventServiceMock.Setup(x => x.UpdateEventAsync(@event)).ReturnsAsync((Event)null);

            var autoMapperMock = new Mock<IMapper>();

            autoMapperMock.Setup(x => x.Map<Event>(eventUpdateRequestDto)).Returns(@event);

            var loggerMock = new Mock<ILogger<EventController>>();
            var eventController = new EventController(
                eventServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            );

            // Act
            var actual = await eventController.UpdateEventAsync(eventUpdateRequestDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actual.Result);
        }

        [Fact]
        public async Task Test_Update_Event_Should_Return_Ok_Result()
        {
            // Arrange
            var eventUpdateRequestDto = new EventUpdateRequestDto {
                Name = "Test"
            };
            var @event = new Event {
                Name = "Test"
            };
            var eventResponseDto = new EventResponseDto {
                Name = "Test"
            };
            var eventServiceMock = new Mock<IEventService>();

            eventServiceMock.Setup(x => x.UpdateEventAsync(@event)).ReturnsAsync(@event);

            var autoMapperMock = new Mock<IMapper>();

            autoMapperMock.Setup(x => x.Map<Event>(eventUpdateRequestDto)).Returns(@event);
            autoMapperMock.Setup(x => x.Map<EventResponseDto>(@event)).Returns(eventResponseDto);

            var loggerMock = new Mock<ILogger<EventController>>();
            var eventController = new EventController(
                eventServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            );

            // Act
            var actual = await eventController.UpdateEventAsync(eventUpdateRequestDto);

            // Assert
            Assert.IsType<OkObjectResult>(actual.Result);
        }
    }
}
