
using Microsoft.Extensions.Configuration;
using Moq;
using TimeScout.Domain.Entities;
using TimeScout.Infrastructure.Repository;
using TimeScout.Application.Services;

namespace TimeScout.Tests.Services
{
    public class IdentityServiceTests
    {
        [Fact]
        public async Task Test_Authenticate_Should_Return_Null()
        {
            // Arrange
            var configurationMock = new Mock<IConfiguration>();
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(x => x.GetUserByEmailAndPasswordAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((User)null);

            var identityService = new IdentityService(userRepositoryMock.Object, configurationMock.Object);

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
            var configurationMock = new Mock<IConfiguration>();
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(x => x.GetUserByEmailAndPasswordAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(user);

            var identityService = new IdentityService(userRepositoryMock.Object, configurationMock.Object);

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
            var configurationMock = new Mock<IConfiguration>();
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(x => x.CheckIfUserExistsAsync(It.IsAny<string>())).ReturnsAsync(true);

            var identityService = new IdentityService(userRepositoryMock.Object, configurationMock.Object);

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
            var configurationMock = new Mock<IConfiguration>();
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(x => x.CheckIfUserExistsAsync(It.IsAny<string>())).ReturnsAsync(false);
            userRepositoryMock.Setup(x => x.CreateUserAsync(It.IsAny<User>())).ReturnsAsync(user);

            var identityService = new IdentityService(userRepositoryMock.Object, configurationMock.Object);

            // Act
            var result = await identityService.CreateUserAsync(user);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Test_Generate_JSON_Web_Token_Should_Return_ArgumentNullException()
        {
            // Arrange
            var configurationMock = new Mock<IConfiguration>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var identityService = new IdentityService(userRepositoryMock.Object, configurationMock.Object);

            // Act
            var exception = Record.Exception(() => identityService.GenerateJSONWebToken("email", "userId", "role"));

            // Assert
            Assert.IsType<ArgumentNullException>(exception);
        }

        [Fact]
        public void Test_Generate_JSON_Web_Token_Should_Return_String()
        {
            // Arrage
            var configurationMock = new Mock<IConfiguration>();

            configurationMock.Setup(x => x["Jwt:Key"]).Returns("nioaeruyg0873qjg3qhgo8fsghfsfgs4");

            var userRepositoryMock = new Mock<IUserRepository>();
            var identityService = new IdentityService(userRepositoryMock.Object, configurationMock.Object);

            // Act
            var result = identityService.GenerateJSONWebToken("email", "userId", "role");

            // Assert
            Assert.IsType<string>(result);
        }

        [Fact]
        public void Test_Generate_Refresh_Token_Should_Return_String()
        {
            
            // Arrage
            var configurationMock = new Mock<IConfiguration>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var identityService = new IdentityService(userRepositoryMock.Object, configurationMock.Object);

            // Act
            var result = identityService.GenerateRefreshToken();

            // Assert
            Assert.IsType<string>(result);
        }

        [Fact]
        public async Task Test_Get_User_By_Refresh_Token_Should_Return_Null()
        {
            // Arrange
            var configurationMock = new Mock<IConfiguration>();
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(x => x.GetUserByRefreshTokenAsync(It.IsAny<string>())).ReturnsAsync((User)null);

            var identityService = new IdentityService(userRepositoryMock.Object, configurationMock.Object);

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
            var configurationMock = new Mock<IConfiguration>();
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(x => x.GetUserByRefreshTokenAsync(It.IsAny<string>())).ReturnsAsync(user);

            var identityService = new IdentityService(userRepositoryMock.Object, configurationMock.Object);

            // Act
            var result = await identityService.GetUserByRefreshTokenAsync("refresh token");

            // Assert
            Assert.IsType<User>(result);
        }

        [Fact]
        public async Task Test_Update_Refresh_Token_Should_Return_False()
        {
            // Arrage
            var configurationMock = new Mock<IConfiguration>();
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(x => x.UpdateRefreshTokenAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(false);

            var identityService = new IdentityService(userRepositoryMock.Object, configurationMock.Object);

            // Act
            var result = await identityService.UpdateRefreshTokenAsync(1, "refresh token");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Test_Update_Refresh_Token_Should_Return_True()
        {
            // Arrage
            var configurationMock = new Mock<IConfiguration>();
            var userRepositoryMock = new Mock<IUserRepository>();

            userRepositoryMock.Setup(x => x.UpdateRefreshTokenAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(true);

            var identityService = new IdentityService(userRepositoryMock.Object, configurationMock.Object);

            // Act
            var result = await identityService.UpdateRefreshTokenAsync(1, "refresh token");

            // Assert
            Assert.True(result);
        }
    }
}