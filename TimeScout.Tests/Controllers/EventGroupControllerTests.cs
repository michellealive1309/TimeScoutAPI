using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TimeScout.API.Controllers;
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
    }
}
