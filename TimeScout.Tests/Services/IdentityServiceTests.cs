using Moq;
using TimeScout.Domain.Entities;
using TimeScout.Application.Services;
using TimeScout.Domain.Interfaces;

namespace TimeScout.Tests.Services
{
    public class IdentityServiceTests
    {
        [Fact]
        public async Task Test_Authenticate_Should_Return_Null()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(x => x.GetUserByEmailAndPasswordAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((User)null);

            var identityService = new IdentityService(userRepositoryMock.Object);

            // Act
            var result = await identityService.AuthenticateAsync("email", "password");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Test_Authenticate_Should_Return_User()
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
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(x => x.GetUserByEmailAndPasswordAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(user);

            var identityService = new IdentityService(userRepositoryMock.Object);

            // Act
            var result = await identityService.AuthenticateAsync(user.Email, user.Password);

            // Assert
            Assert.Equal(user, result);
        }

        [Fact]
        public async Task Test_Create_User_Should_Return_False()
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
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(x => x.CheckIfUserExistsAsync(It.IsAny<string>())).ReturnsAsync(true);

            var identityService = new IdentityService(userRepositoryMock.Object);

            // Act
            var result = await identityService.CreateUserAsync(user);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Test_Create_User_Should_Return_True()
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
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(x => x.CheckIfUserExistsAsync(It.IsAny<string>())).ReturnsAsync(false);

            var identityService = new IdentityService(userRepositoryMock.Object);

            // Act
            var result = await identityService.CreateUserAsync(user);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Test_Get_User_By_Refresh_Token_Should_Return_Null()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(x => x.GetUserByRefreshTokenAsync(It.IsAny<string>())).ReturnsAsync((User)null);

            var identityService = new IdentityService(userRepositoryMock.Object);

            // Act
            var result = await identityService.GetUserByRefreshTokenAsync("refresh token");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Test_Get_User_By_Refresh_Token_Should_Return_User()
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
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(x => x.GetUserByRefreshTokenAsync(It.IsAny<string>())).ReturnsAsync(user);

            var identityService = new IdentityService(userRepositoryMock.Object);

            // Act
            var result = await identityService.GetUserByRefreshTokenAsync("refresh token");

            // Assert
            Assert.IsType<User>(result);
        }

        [Fact]
        public async Task Test_Update_Refresh_Token_Should_Return_False()
        {
            // Arrage
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(x => x.UpdateRefreshTokenAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(false);

            var identityService = new IdentityService(userRepositoryMock.Object);

            // Act
            var result = await identityService.UpdateRefreshTokenAsync(1, "refresh token");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Test_Update_Refresh_Token_Should_Return_True()
        {
            // Arrage
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(x => x.UpdateRefreshTokenAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(true);

            var identityService = new IdentityService(userRepositoryMock.Object);

            // Act
            var result = await identityService.UpdateRefreshTokenAsync(1, "refresh token");

            // Assert
            Assert.True(result);
        }
    }
}