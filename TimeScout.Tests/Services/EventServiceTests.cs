using Moq;
using TimeScout.Domain.Entities;
using TimeScout.Application.Services;
using TimeScout.Domain.Interfaces;

namespace TimeScout.Tests.Services
{
    public class EventServiceTests
    {
        [Fact]
        public async Task Test_Create_Event_Should_Return_False()
        {
            // Arrange
            var @event = new Event {
                Name = "Test"
            };
            var eventRepositoryMock = new Mock<IEventRepository>();
            var eventService = new EventService(eventRepositoryMock.Object);

            // Act
            var actual = await eventService.CreateEventAsync(@event);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public async Task Test_Create_Event_Should_Return_True()
        {
            // Arrange
            var @event = new Event {
                Name = "Test",
                UserId = 1
            };
            var eventRepositoryMock = new Mock<IEventRepository>();
            var eventService = new EventService(eventRepositoryMock.Object);

            // Act
            var actual = await eventService.CreateEventAsync(@event);

            // Assert
            Assert.True(actual);
            eventRepositoryMock.Verify(x => x.AddAsync(@event), Times.Once());
        }

        [Fact]
        public async Task Test_Delete_Event_Should_Return_False()
        {
            // Arrange
            var eventRepositoryMock = new Mock<IEventRepository>();

            eventRepositoryMock.Setup(x => x.FindAsync(1)).ReturnsAsync((Event)null);

            var eventService = new EventService(eventRepositoryMock.Object);

            // Act
            var actual = await eventService.DeleteEventAsync(1, 1);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public async Task Test_Delete_Event_UserId_Invalid_Should_Return_False()
        {
            // Arrange
            var @event = new Event {
                Name = "Test",
                UserId = 2
            };
            var eventRepositoryMock = new Mock<IEventRepository>();

            eventRepositoryMock.Setup(x => x.FindAsync(1)).ReturnsAsync(@event);

            var eventService = new EventService(eventRepositoryMock.Object);

            // Act
            var actual = await eventService.DeleteEventAsync(1, 1);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public async Task Test_Delete_Event_Should_Return_True()
        {
            // Arrange
            var @event = new Event {
                Name = "Test",
                UserId = 1
            };
            var eventRepositoryMock = new Mock<IEventRepository>();

            eventRepositoryMock.Setup(x => x.FindAsync(1)).ReturnsAsync(@event);

            var eventService = new EventService(eventRepositoryMock.Object);

            // Act
            var actual = await eventService.DeleteEventAsync(1, 1);

            // Assert
            Assert.True(actual);
        }

        [Theory]
        [InlineData("")]
        [InlineData("day")]
        [InlineData("week")]
        [InlineData("biweek")]
        [InlineData("month")]
        [InlineData("year")]
        public async Task Test_Get_All_Event_Should_Return_Event_List(string span)
        {
            // Arrange
            DateTime now = DateTime.UtcNow;
            var expected = new List<Event> {
                new() {
                    Name = "Test",
                    StartDate = DateOnly.FromDateTime(DateTime.UtcNow),
                    StartTime = TimeOnly.FromDateTime(DateTime.UtcNow),
                    EndDate = DateOnly.FromDateTime(DateTime.UtcNow)
                }
            };
            var eventRepositoryMock = new Mock<IEventRepository>();

            eventRepositoryMock.Setup(x => x.GetEventsByDateRangeAsync(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).ReturnsAsync(expected);

            var eventService = new EventService(eventRepositoryMock.Object);

            // Act
            var actual = await eventService.GetAllEventsAsync(span, now);

            // Assert
            Assert.Same(expected, actual);
        }

        [Fact]
        public async Task Test_Get_Event_By_Id_Should_Return_Null()
        {
            // Arrange
            var eventRepositoryMock = new Mock<IEventRepository>();

            eventRepositoryMock.Setup(x => x.GetEventByIdAsync(1)).ReturnsAsync((Event)null);

            var eventService = new EventService(eventRepositoryMock.Object);

            // Act
            var actual = await eventService.GetEventByIdAsync(1);

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public async Task Test_Get_Event_By_Id_Should_Return_Event()
        {
            // Arrange
            var @event = new Event {
                Id = 1,
                Name = "Test"
            };
            var eventRepositoryMock = new Mock<IEventRepository>();

            eventRepositoryMock.Setup(x => x.GetEventByIdAsync(1)).ReturnsAsync(@event);

            var eventService = new EventService(eventRepositoryMock.Object);

            // Act
            var actual = await eventService.GetEventByIdAsync(1);

            // Assert
            Assert.IsType<Event>(actual);
        }

        [Fact]
        public async Task Test_Update_Event_Should_Return_Null()
        {
            // Arrange
            var @event = new Event {
                Name = "Test"
            };
            var eventRepositoryMock = new Mock<IEventRepository>();

            eventRepositoryMock.Setup(x => x.GetEventByIdAsync(1)).ReturnsAsync((Event)null);

            var eventService = new EventService(eventRepositoryMock.Object);

            // Act
            var actual = await eventService.UpdateEventAsync(@event);

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public async Task Test_Update_Event_UserId_Invalid_Should_Return_Null()
        {
            // Arrange
            var @event = new Event {
                Name = "Test"
            };
            var eventRepositoryMock = new Mock<IEventRepository>();

            eventRepositoryMock.Setup(x => x.GetEventByIdAsync(1)).ReturnsAsync((Event)null);

            var eventService = new EventService(eventRepositoryMock.Object);

            // Act
            var actual = await eventService.UpdateEventAsync(@event);

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public async Task Test_Update_Event_Should_Return_Event()
        {
            // Arrange
            var toUpdateEvent = new Event {
                Id = 1,
                Name = "Test changed",
                UserId = 1
            };
            var @event = new Event {
                Id = 1,
                Name = "Test",
                UserId = 1
            };
            var eventRepositoryMock = new Mock<IEventRepository>();

            eventRepositoryMock.Setup(x => x.GetEventByIdAsync(1)).ReturnsAsync(@event);

            var eventService = new EventService(eventRepositoryMock.Object);

            // Act
            var actual = await eventService.UpdateEventAsync(toUpdateEvent);

            // Assert
            Assert.IsType<Event>(actual);
            eventRepositoryMock.Verify(x => x.UpdateAsync(@event), Times.Once());
        }
    }
}
