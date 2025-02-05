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

        [Fact]
        public async Task Test_Create_Event_Group_Should_Return_False()
        {
            // Arrange
            var eventGroup = new EventGroup {
                Name = "Test"
            };
            var eventGroupRepositoryMock = new Mock<IEventGroupRepository>();
            var eventGroupService = new EventGroupService(eventGroupRepositoryMock.Object);

            // Act
            var actual = await eventGroupService.CreateEventGroupAsync(eventGroup);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public async Task Test_Create_Event_Group_Should_Return_True()
        {
            // Arrange
            var eventGroup = new EventGroup {
                Name = "Test",
                Members = [
                    new() {
                        Id = 1,
                        Username = "Tester",
                        Email = "john.doe@mail.com",
                        Password = "Test",
                        Role = "User"
                    }
                ]
            };
            var eventGroupRepositoryMock = new Mock<IEventGroupRepository>();

            eventGroupRepositoryMock.Setup(x => x.AddAsync(eventGroup)).Verifiable();

            var eventGroupService = new EventGroupService(eventGroupRepositoryMock.Object);

            // Act
            var actual = await eventGroupService.CreateEventGroupAsync(eventGroup);

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public async Task Test_Delete_Event_Group_Should_Return_False()
        {
            // Arrange
            var eventGroupRepositoryMock = new Mock<IEventGroupRepository>();

            eventGroupRepositoryMock.Setup(x => x.FindEventGroupWithMemberByIdAsync(1)).ReturnsAsync((EventGroup)null);

            var eventGroupService = new EventGroupService(eventGroupRepositoryMock.Object);

            // Act
            var actual = await eventGroupService.DeleteEventGroupAsync(1, 1);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public async Task Test_Delete_Event_Group_Should_Return_True()
        {
            // Arrange
            var eventGroup = new EventGroup {
                Name = "Test",
                Members = [
                    new() {
                        Id = 1,
                        Username = "Tester",
                        Email = "john.doe@mail.com",
                        Password = "Test",
                        Role = "User"
                    }
                ]
            };
            var eventGroupRepositoryMock = new Mock<IEventGroupRepository>();

            eventGroupRepositoryMock.Setup(x => x.FindEventGroupWithMemberByIdAsync(1)).ReturnsAsync(eventGroup);
            eventGroupRepositoryMock.Setup(x => x.RemoveAsync(eventGroup)).Verifiable();

            var eventGroupService = new EventGroupService(eventGroupRepositoryMock.Object);

            // Act
            var actual = await eventGroupService.DeleteEventGroupAsync(1, 1);

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public async Task Test_Update_Event_Group_Null_Event_Group_Should_Return_Null()
        {
            // Arrange
            var eventGroup = new EventGroup {
                Name = "Test",
                Members = [
                    new() {
                        Id = 1,
                        Username = "Tester",
                        Email = "john.doe@mail.com",
                        Password = "Test",
                        Role = "User"
                    }
                ]
            };
            var eventGroupRepositoryMock = new Mock<IEventGroupRepository>();

            eventGroupRepositoryMock.Setup(x => x.FindEventGroupWithMemberByIdAsync(1)).ReturnsAsync((EventGroup)null);

            var eventGroupService = new EventGroupService(eventGroupRepositoryMock.Object);

            // Act
            var actual = await eventGroupService.UpdateEventGroupAsync(eventGroup, 1);

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public async Task Test_Update_Event_Group_New_Event_Group_Empty_Member_Should_Return_Null()
        {
            // Arrange
            var eventGroup = new EventGroup {
                Id = 1,
                Name = "Test new"
            };
            var toUpdateEventGroup = new EventGroup {
                Id = 1,
                Name = "Test old",
                Members = [
                    new() {
                        Id = 1,
                        Username = "Tester",
                        Email = "john.doe@mail.com",
                        Password = "Test",
                        Role = "User"
                    }
                ]
            };
            var eventGroupRepositoryMock = new Mock<IEventGroupRepository>();

            eventGroupRepositoryMock.Setup(x => x.FindEventGroupWithMemberByIdAsync(1)).ReturnsAsync(toUpdateEventGroup);

            var eventGroupService = new EventGroupService(eventGroupRepositoryMock.Object);

            // Act
            var actual = await eventGroupService.UpdateEventGroupAsync(eventGroup, 1);

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public async Task Test_Update_Event_Group_Old_Event_Gruop_Empty_Member_Should_Return_Null()
        {
            // Arrange
            var eventGroup = new EventGroup {
                Id = 1,
                Name = "Test new",
                Members = [
                    new() {
                        Id = 1,
                        Username = "Tester",
                        Email = "john.doe@mail.com",
                        Password = "Test",
                        Role = "User"
                    }
                ]
            };
            var toUpdateEventGroup = new EventGroup {
                Id = 1,
                Name = "Test old"
            };
            var eventGroupRepositoryMock = new Mock<IEventGroupRepository>();

            eventGroupRepositoryMock.Setup(x => x.FindEventGroupWithMemberByIdAsync(1)).ReturnsAsync(toUpdateEventGroup);

            var eventGroupService = new EventGroupService(eventGroupRepositoryMock.Object);

            // Act
            var actual = await eventGroupService.UpdateEventGroupAsync(eventGroup, 1);

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public async Task Test_Update_Event_Group_Current_User_Is_Not_Member_Should_Return_Null()
        {
            // Arrange
            var eventGroup = new EventGroup {
                Id = 1,
                Name = "Test new",
                Members = [
                    new() {
                        Id = 1,
                        Username = "Tester",
                        Email = "john.doe@mail.com",
                        Password = "Test",
                        Role = "User"
                    }
                ]
            };
            var toUpdateEventGroup = new EventGroup {
                Id = 1,
                Name = "Test old",
                Members = [
                    new() {
                        Id = 2,
                        Username = "Will Smith",
                        Email = "will.smith@mail.com",
                        Password = "Tester",
                        Role = "User"
                    }
                ]
            };
            var eventGroupRepositoryMock = new Mock<IEventGroupRepository>();

            eventGroupRepositoryMock.Setup(x => x.FindEventGroupWithMemberByIdAsync(1)).ReturnsAsync(toUpdateEventGroup);

            var eventGroupService = new EventGroupService(eventGroupRepositoryMock.Object);

            // Act
            var actual = await eventGroupService.UpdateEventGroupAsync(eventGroup, 1);

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public async Task Test_Update_Event_Group_Success_Should_Return_Event_Group()
        {
            // Arrange
            var eventGroup = new EventGroup() {
                Id = 1,
                Name = "Test new",
                Members = [
                    new() {
                        Id = 1,
                        Username = "Tester",
                        Email = "john.doe@mail.com",
                        Password = "Test",
                        Role = "User"
                    }
                ]
            };
            var toUpdateEventGroup = new EventGroup() {
                Id = 1,
                Name = "Test old",
                Members = [
                    new() {
                        Id = 1,
                        Username = "Tester",
                        Email = "john.doe@mail.com",
                        Password = "Test",
                        Role = "User"
                    }
                ]
            };
            var eventGroupRepositoryMock = new Mock<IEventGroupRepository>();

            eventGroupRepositoryMock.Setup(x => x.FindEventGroupWithMemberByIdAsync(1)).ReturnsAsync(toUpdateEventGroup);
            eventGroupRepositoryMock.Setup(x => x.GetMembersAsync(eventGroup.Members)).ReturnsAsync(eventGroup.Members);

            var eventGroupService = new EventGroupService(eventGroupRepositoryMock.Object);

            // Act
            var actual = await eventGroupService.UpdateEventGroupAsync(eventGroup, 1);

            // Assert
            Assert.IsType<EventGroup>(actual);
            eventGroupRepositoryMock.Verify(x => x.UpdateAsync(toUpdateEventGroup), Times.Once());
        }
    }
}
