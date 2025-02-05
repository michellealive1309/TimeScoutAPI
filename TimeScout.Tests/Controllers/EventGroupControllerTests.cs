using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TimeScout.API.Controllers;
using TimeScout.API.DTOs.EventGroup;
using TimeScout.API.Models;
using TimeScout.API.Services;

namespace TimeScout.Tests.Controllers
{
    public class EventGroupControllerTests
    {
        [Fact]
        public async Task Test_Get_Event_By_Id_Should_Return_NotFound_Result()
        {
            // Arrange
            var eventGroupServiceMock = new Mock<IEventGroupService>();

            eventGroupServiceMock.Setup(x => x.GetEventGroupByIdAsync(1, 1)).ReturnsAsync((EventGroup)null);

            var autoMapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger<EventGroupController>>();
            var controllerContext = new ControllerContext() {
                HttpContext = new DefaultHttpContext() {
                    User = new ClaimsPrincipal(new ClaimsIdentity([
                        new Claim(ClaimTypes.NameIdentifier, "1")
                    ]))
                }
            };
            var eventGroupController = new EventGroupController(
                eventGroupServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            ) {
                ControllerContext = controllerContext
            };

            // Act
            var actual = await eventGroupController.GetEventGroupByIdAsync(1);

            // Assert
            Assert.IsType<NotFoundResult>(actual.Result);
        }

        [Fact]
        public async Task Test_Get_Event_By_Id_Should_Return_Ok_Result()
        {
            // Arrange
            var eventGroupServiceMock = new Mock<IEventGroupService>();

            eventGroupServiceMock.Setup(x => x.GetEventGroupByIdAsync(1, 1)).ReturnsAsync(new EventGroup { Name = "Test" });

            var autoMapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger<EventGroupController>>();
            var controllerContext = new ControllerContext() {
                HttpContext = new DefaultHttpContext() {
                    User = new ClaimsPrincipal(new ClaimsIdentity([
                        new Claim(ClaimTypes.NameIdentifier, "1")
                    ]))
                }
            };
            var eventGroupController = new EventGroupController(
                eventGroupServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            ) {
                ControllerContext = controllerContext
            };

            // Act
            var actual = await eventGroupController.GetEventGroupByIdAsync(1);

            // Assert
            Assert.IsType<OkObjectResult>(actual.Result);
        }

        [Fact]
        public async Task Test_Get_All_Event_Group_Should_Return_Ok_Result()
        {
            // Arrange
            var eventGroups = new List<EventGroup> {new() { Name = "Test" }};
            var eventGroupServiceMock = new Mock<IEventGroupService>();

            eventGroupServiceMock.Setup(x => x.GetAllEventGroupAsync(1)).ReturnsAsync(eventGroups);

            var autoMapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger<EventGroupController>>();
            var controllerContext = new ControllerContext() {
                HttpContext = new DefaultHttpContext() {
                    User = new ClaimsPrincipal(new ClaimsIdentity([
                        new Claim(ClaimTypes.NameIdentifier, "1")
                    ]))
                }
            };
            var eventGroupController = new EventGroupController(
                eventGroupServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            ) {
                ControllerContext = controllerContext
            };

            // Act
            var actual = await eventGroupController.GetAllEventGroupAsync();

            // Assert
            Assert.IsType<OkObjectResult>(actual.Result);
        }

        [Fact]
        public async Task Test_Create_Event_Group_Model_Invalid_Should_Return_BadRequest_Result()
        {
            // Arrange
            var eventGroupCreate = new EventGroupCreateRequestDto {
                Name = ""
            };
            var eventGroupServiceMock = new Mock<IEventGroupService>();
            var autoMapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger<EventGroupController>>();
            var eventGroupController = new EventGroupController(
                eventGroupServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            );

            // Act
            var actual = await eventGroupController.CreateEventGroupAsync(eventGroupCreate);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actual);
        }

        [Fact]
        public async Task Test_Create_Event_Group_Should_Return_BadRequest_Result()
        {
            // Arrange
            var eventGroupCreate = new EventGroupCreateRequestDto {
                Name = "Test"
            };
            var eventGroup = new EventGroup {
                Name = "Test"
            };
            var eventGroupServiceMock = new Mock<IEventGroupService>();

            eventGroupServiceMock.Setup(x => x.CreateEventGroupAsync(eventGroup)).ReturnsAsync(false);

            var autoMapperMock = new Mock<IMapper>();

            autoMapperMock.Setup(x => x.Map<EventGroup>(eventGroupCreate)).Returns(eventGroup);

            var loggerMock = new Mock<ILogger<EventGroupController>>();
            var eventGroupController = new EventGroupController(
                eventGroupServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            );

            // Act
            var actual = await eventGroupController.CreateEventGroupAsync(eventGroupCreate);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actual);
        }

        [Fact]
        public async Task Test_Create_Event_Group_Should_Return_Ok_Result()
        {
            // Arrange
            var eventGroupCreate = new EventGroupCreateRequestDto {
                Name = "Test"
            };
            var eventGroup = new EventGroup {
                Name = "Test"
            };
            var eventGroupServiceMock = new Mock<IEventGroupService>();

            eventGroupServiceMock.Setup(x => x.CreateEventGroupAsync(eventGroup)).ReturnsAsync(true);

            var autoMapperMock = new Mock<IMapper>();

            autoMapperMock.Setup(x => x.Map<EventGroup>(eventGroupCreate)).Returns(eventGroup);

            var loggerMock = new Mock<ILogger<EventGroupController>>();
            var eventGroupController = new EventGroupController(
                eventGroupServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            );

            // Act
            var actual = await eventGroupController.CreateEventGroupAsync(eventGroupCreate);

            // Assert
            Assert.IsType<OkObjectResult>(actual);
        }

        [Fact]
        public async Task Test_Delete_Event_Group_Should_Return_BadRequest_Result()
        {
            // Arrange
            var eventGroupServiceMock = new Mock<IEventGroupService>();

            eventGroupServiceMock.Setup(x => x.DeleteEventGroupAsync(1, 1)).ReturnsAsync(false);

            var autoMapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger<EventGroupController>>();
            var controllerContext = new ControllerContext() {
                HttpContext = new DefaultHttpContext() {
                    User = new ClaimsPrincipal(new ClaimsIdentity([
                        new Claim(ClaimTypes.NameIdentifier, "1")
                    ]))
                }
            };
            var eventGroupController = new EventGroupController(
                eventGroupServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            ) {
                ControllerContext = controllerContext
            };

            // Act
            var actual = await eventGroupController.DeleteEventGroupAsync(1);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actual);
        }

        [Fact]
        public async Task Test_Delete_Event_Group_Should_Return_Ok_Result()
        {
            // Arrange
            var eventGroupServiceMock = new Mock<IEventGroupService>();

            eventGroupServiceMock.Setup(x => x.DeleteEventGroupAsync(1, 1)).ReturnsAsync(true);

            var autoMapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger<EventGroupController>>();
            var controllerContext = new ControllerContext() {
                HttpContext = new DefaultHttpContext() {
                    User = new ClaimsPrincipal(new ClaimsIdentity([
                        new Claim(ClaimTypes.NameIdentifier, "1")
                    ]))
                }
            };
            var eventGroupController = new EventGroupController(
                eventGroupServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            ) {
                ControllerContext = controllerContext
            };

            // Act
            var actual = await eventGroupController.DeleteEventGroupAsync(1);

            // Assert
            Assert.IsType<OkObjectResult>(actual);
        }

        [Fact]
        public async Task Test_Update_Event_Group_Model_Invaid_Should_Return_BadRequest_Result()
        {
            // Arrange
            var eventGroupUpdate = new EventGroupUpdateRequestDto {
                Id = 1,
                Name = ""
            };
            var eventGroupServiceMock = new Mock<IEventGroupService>();
            var autoMapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger<EventGroupController>>();
            var controllerContext = new ControllerContext() {
                HttpContext = new DefaultHttpContext() {
                    User = new ClaimsPrincipal(new ClaimsIdentity([
                        new Claim(ClaimTypes.NameIdentifier, "1")
                    ]))
                }
            };
            var eventGroupController = new EventGroupController(
                eventGroupServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            ) {
                ControllerContext = controllerContext
            };

            // Act
            var actual = await eventGroupController.UpdateEventGroupAsync(eventGroupUpdate);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actual.Result);
        }

        [Fact]
        public async Task Test_Update_Event_Group_Should_Return_BadRequest_Result()
        {
            // Arrange
            var eventGroupUpdate = new EventGroupUpdateRequestDto {
                Id = 1,
                Name = "Test"
            };
            var eventGroup = new EventGroup {
                Id = 1,
                Name = "Test"
            };
            var eventGroupServiceMock = new Mock<IEventGroupService>();

            eventGroupServiceMock.Setup(x => x.UpdateEventGroupAsync(eventGroup, 1)).ReturnsAsync((EventGroup)null);

            var autoMapperMock = new Mock<IMapper>();

            autoMapperMock.Setup(x => x.Map<EventGroup>(eventGroupUpdate)).Returns(eventGroup);

            var loggerMock = new Mock<ILogger<EventGroupController>>();
            var controllerContext = new ControllerContext() {
                HttpContext = new DefaultHttpContext() {
                    User = new ClaimsPrincipal(new ClaimsIdentity([
                        new Claim(ClaimTypes.NameIdentifier, "1")
                    ]))
                }
            };
            var eventGroupController = new EventGroupController(
                eventGroupServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            ) {
                ControllerContext = controllerContext
            };

            // Act
            var actual = await eventGroupController.UpdateEventGroupAsync(eventGroupUpdate);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actual.Result);
        }

        [Fact]
        public async Task Test_Update_Event_Group_Should_Return_EventGroupResponseDto_Result()
        {
            // Arrange
            var eventGroupUpdate = new EventGroupUpdateRequestDto {
                Id = 1,
                Name = "Test"
            };
            var eventGroup = new EventGroup {
                Id = 1,
                Name = "Test"
            };
            var eventGroupResponse = new EventGroupResponseDto {
                Id = 1,
                Name = "Test"
            };
            var eventGroupServiceMock = new Mock<IEventGroupService>();

            eventGroupServiceMock.Setup(x => x.UpdateEventGroupAsync(eventGroup, 1)).ReturnsAsync(eventGroup);

            var autoMapperMock = new Mock<IMapper>();

            autoMapperMock.Setup(x => x.Map<EventGroup>(eventGroupUpdate)).Returns(eventGroup);
            autoMapperMock.Setup(x => x.Map<EventGroupResponseDto>(eventGroup)).Returns(eventGroupResponse);

            var loggerMock = new Mock<ILogger<EventGroupController>>();
            var controllerContext = new ControllerContext() {
                HttpContext = new DefaultHttpContext() {
                    User = new ClaimsPrincipal(new ClaimsIdentity([
                        new Claim(ClaimTypes.NameIdentifier, "1")
                    ]))
                }
            };
            var eventGroupController = new EventGroupController(
                eventGroupServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            ) {
                ControllerContext = controllerContext
            };

            // Act
            var actual = await eventGroupController.UpdateEventGroupAsync(eventGroupUpdate);

            // Assert
            Assert.IsType<OkObjectResult>(actual.Result);
        }
    }
}
