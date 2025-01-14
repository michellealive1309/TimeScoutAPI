using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TimeScout.API.Controllers;
using TimeScout.API.DTOs.Authentication;
using TimeScout.API.DTOs.Login;
using TimeScout.API.Models;
using TimeScout.API.Services;

namespace TimeScout.Tests.Controllers
{
    public class AuthenticationControllerTests
    {
        [Fact]
        public async Task Test_Login_Return_Unauthorized_Result()
        {
            // Arrange
            var loginRequestDto = new LoginRequestDto();
            var IdentityServiceMock = new Mock<IIdentityService>();

            IdentityServiceMock.Setup(x => x.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((User)null);

            var AutoMapperMock = new Mock<IMapper>();
            var LoggerMock = new Mock<ILogger<AuthenticationController>>();
            var authenticationController = new AuthenticationController(
                IdentityServiceMock.Object,
                AutoMapperMock.Object,
                LoggerMock.Object
            );

            // Act
            var result = await authenticationController.Login(loginRequestDto);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task Test_Login_Return_Ok_Result()
        {
            // Arrange
            var loginRequestDto = new LoginRequestDto();
            var user = CreateUser();
            var identityServiceMock = new Mock<IIdentityService>();

            identityServiceMock.Setup(x => x.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(user);
            identityServiceMock.Setup(x => x.GenerateJSONWebToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(It.IsAny<string>());
            identityServiceMock.Setup(x => x.GenerateRefreshToken()).Returns(It.IsAny<string>());
            identityServiceMock.Setup(x => x.UpdateRefreshTokenAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(true);

            var autoMapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger<AuthenticationController>>();
            var authenticationController = new AuthenticationController(
                identityServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            );

            // Act
            var result = await authenticationController.Login(loginRequestDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Test_Refresh_Return_Unauthorized_Result()
        {
            // Arrange
            var refreshRequestDto = new RefreshRequestDto();
            var identityServiceMock = new Mock<IIdentityService>();

            identityServiceMock.Setup(x => x.GetUserByRefreshTokenAsync(It.IsAny<string>())).ReturnsAsync((User)null);

            var autoMapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger<AuthenticationController>>();
            var authenticationController = new AuthenticationController(
                identityServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            );

            // Act
            var result = await authenticationController.Refresh(refreshRequestDto);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task Test_Refresh_Return_Ok_Result()
        {
            // Arrange
            var refreshRequestDto = new RefreshRequestDto();
            var user = CreateUser();
            var identityServiceMock = new Mock<IIdentityService>();

            identityServiceMock.Setup(x => x.GetUserByRefreshTokenAsync(It.IsAny<string>())).ReturnsAsync(user);
            identityServiceMock.Setup(x => x.GenerateJSONWebToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(It.IsAny<string>());
            identityServiceMock.Setup(x => x.GenerateRefreshToken()).Returns(It.IsAny<string>());
            identityServiceMock.Setup(x => x.UpdateRefreshTokenAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(true);

            var autoMapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger<AuthenticationController>>();
            var authenticationController = new AuthenticationController(
                identityServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            );

            // Act
            var result = await authenticationController.Refresh(refreshRequestDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Test_Register_Validation_Fail_Return_BadRequest_Result()
        {
            // Arrange
            var registerRequestDto = new RegisterRequestDto {
                Email = "john.doe@mail.com",
                FirstName = "John",
                LastName = "Doe"
            };
            var identityServiceMock = new Mock<IIdentityService>();
            var autoMapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger<AuthenticationController>>();
            var authenticationController = new AuthenticationController(
                identityServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            );

            authenticationController.ModelState.AddModelError("Username", "Username is required");
            authenticationController.ModelState.AddModelError("Password", "Password is required");

            // Act
            var result = await authenticationController.Register(registerRequestDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Test_Register_User_Existed_Return_BadRequest_Result()
        {
            // Arrange
            var registerRequestDto = new RegisterRequestDto();
            var identityServiceMock = new Mock<IIdentityService>();

            identityServiceMock.Setup(x => x.CreateUserAsync(It.IsAny<User>())).ReturnsAsync(false);

            var autoMapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger<AuthenticationController>>();
            var authenticationController = new AuthenticationController(
                identityServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            );

            // Act
            var result = await authenticationController.Register(registerRequestDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Test_Register_Success_Return_Ok_Result()
        {
            // Arrange
            var registerRequestDto = new RegisterRequestDto();
            var identityServiceMock = new Mock<IIdentityService>();

            identityServiceMock.Setup(x => x.CreateUserAsync(It.IsAny<User>())).ReturnsAsync(true);

            var autoMapperMock = new Mock<IMapper>();

            autoMapperMock.Setup(x => x.Map<User>(registerRequestDto)).Returns(It.IsAny<User>());

            var loggerMock = new Mock<ILogger<AuthenticationController>>();
            var authenticationController = new AuthenticationController(
                identityServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            );

            // Act
            var result = await authenticationController.Register(registerRequestDto);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        private User CreateUser()
        {
            return new User
            {
                Id = 1,
                Username = "testuser",
                Email = "testuser@example.com",
                Password = "hashedpassword",
                Role = "User",
                FirstName = "Test",
                LastName = "User",
                RefreshToken = "",
                RecoveryEndDate = DateTime.UtcNow,
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow
            };
        }       
    }
}
