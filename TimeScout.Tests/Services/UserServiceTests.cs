using Moq;
using TimeScout.Domain.Entities;
using TimeScout.Application.Services;
using TimeScout.Domain.Interfaces;

namespace TimeScout.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task Test_Delete_Refresh_Token_Should_Return_False()
        {
            // Arrange
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.DeleteRefreshTokenAsync(It.IsAny<int>())).ReturnsAsync(false);

            var userService = new UserService(userRepository.Object);

            // Act
            var result = await userService.DeleteRefreshTokenAsync(1);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Test_Delete_Refresh_Token_Should_Return_True()
        {
            // Arrange
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.DeleteRefreshTokenAsync(It.IsAny<int>())).ReturnsAsync(true);

            var userService = new UserService(userRepository.Object);

            // Act
            var result = await userService.DeleteRefreshTokenAsync(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Test_Delete_User_Should_Return_False()
        {
            // Arrange
            var userRepository = new Mock<IUserRepository>();
            var userService = new UserService(userRepository.Object);

            // Act
            var result = await userService.DeleteUserAsync(1);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Test_Delete_User_Should_Return_True()
        {
            // Arrange
            var user = new User {
                Id = 1,
                Username = "test",
                Email = "john.doe@mail.com",
                Password = "abc",
                Role = "User"
            };
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.FindAsync(1)).ReturnsAsync(user);
            var userService = new UserService(userRepository.Object);

            // Act
            var result = await userService.DeleteUserAsync(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Test_Get_User_By_Id_Should_Return_Null()
        {
            // Arrange
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync((User)null);

            var userService = new UserService(userRepository.Object);

            // Act
            var result = await userService.GetUserByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Test_Get_User_By_Id_Should_Return_User()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                Username = "test",
                Email = "test@mail.com",
                Password = "password",
                Role = "User"
            };

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync(user);

            var userService = new UserService(userRepository.Object);

            // Act
            var result = await userService.GetUserByIdAsync(1);

            // Assert
            Assert.Equal(user, result);
        }

        [Fact]
        public async Task Test_Recover_User_User_Null_Should_Return_False()
        {
            // Arrange
            var userRepository = new Mock<IUserRepository>();

            userRepository.Setup(x => x.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync((User)null);

            var userService = new UserService(userRepository.Object);

            // Act
            var result = await userService.RecoverUserAsync(1);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Test_Recover_User_Recovery_End_Date_Passed_Should_Return_False()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                Username = "test",
                Email = "john.doe@mail.com",
                Password = "password",
                Role = "User",
                RecoveryEndDate = DateTime.UtcNow.AddDays(-1)
            };
            var userRepository = new Mock<IUserRepository>();

            userRepository.Setup(x => x.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync(user);

            var userService = new UserService(userRepository.Object);

            // Act
            var result = await userService.RecoverUserAsync(1);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Test_Recover_User_Should_Return_True()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                Username = "test",
                Email = "john.doe@mail.com",
                Password = "password",
                Role = "User",
                RecoveryEndDate = DateTime.UtcNow.AddDays(+1)
            };
            var userRepository = new Mock<IUserRepository>();

            userRepository.Setup(x => x.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync(user);

            var userService = new UserService(userRepository.Object);

            // Act
            var result = await userService.RecoverUserAsync(1);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Test_Update_User_Should_Return_Null()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                Username = "test",
                Email = "john.doe@mail.com",
                Password = "password",
                Role = "User",
            };
            var userRepository = new Mock<IUserRepository>();
            var userService = new UserService(userRepository.Object);

            // Act
            var result = await userService.UpdateUserAsync(user);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Test_Update_User_Should_Return_User()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                Username = "test",
                Email = "john.doe@mail.com",
                Password = "password",
                Role = "User",
                RecoveryEndDate = DateTime.UtcNow.AddDays(+1)
            };
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.FindAsync(1)).ReturnsAsync(user);
            var userService = new UserService(userRepository.Object);

            // Act
            var result = await userService.UpdateUserAsync(user);

            // Assert
            Assert.Equivalent(user, result);
        }
    }
}