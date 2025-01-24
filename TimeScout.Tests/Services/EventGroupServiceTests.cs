using Moq;
using TimeScout.API.Models;
using TimeScout.API.Repository;
using TimeScout.API.Services;

namespace TimeScout.Tests.Services
{
    public class EventGroupServiceTests
    {
        [Fact]
        public async Task Test_Get_Event_Group_By_Id_Should_Return_Null()
        {
            // Arrange
            var eventGroupRepositoryMock = new Mock<IEventGroupRepository>();

            eventGroupRepositoryMock.Setup(x => x.GetEventGroupByIdAsync(1, 1)).ReturnsAsync((EventGroup)null);

            var eventGroupService = new EventGroupService(eventGroupRepositoryMock.Object);

            // Act
            var actual = await eventGroupService.GetEventGroupByIdAsync(1, 1);

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public async Task Test_Get_Event_Group_By_Id_Should_Return_Event()
        {
            // Arrange
            var expected = new EventGroup { Name = "Test" };
            var eventGroupRepositoryMock = new Mock<IEventGroupRepository>();

            eventGroupRepositoryMock.Setup(x => x.GetEventGroupByIdAsync(1, 1)).ReturnsAsync(expected);

            var eventGroupService = new EventGroupService(eventGroupRepositoryMock.Object);

            // Act
            var actual = await eventGroupService.GetEventGroupByIdAsync(1, 1);

            // Assert
            Assert.Same(expected, actual);
        }

        [Fact]
        public async Task Test_Get_All_Event_Group_Should_Return_Event()
        {
            // Arrange
            var expected = new List<EventGroup> { new() { Name = "Test" }};
            var eventGroupRepositoryMock = new Mock<IEventGroupRepository>();

            eventGroupRepositoryMock.Setup(x => x.GetAllEventGroupAsync(1)).ReturnsAsync(expected);

            var eventGroupService = new EventGroupService(eventGroupRepositoryMock.Object);

            // Act
            var actual = await eventGroupService.GetAllEventGroupAsync(1);

            // Assert
            Assert.Same(expected, actual);
        }
    }
}
