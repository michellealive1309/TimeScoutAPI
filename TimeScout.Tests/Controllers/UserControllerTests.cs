

using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TimeScout.API.Controllers;
using TimeScout.API.DTOs.User;
using TimeScout.API.Models;
using TimeScout.API.Services;

namespace TimeScout.Tests.Controllers
{
    public class UserControllerTests
    {
        [Fact]
        public async Task Test_Delete_User_Fail_Return_BadRequest_Result()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();

            userServiceMock.Setup(x => x.DeleteUserAsync(It.IsAny<int>())).ReturnsAsync(false);

            var autoMapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger<UserController>>();
            var controllerContext = new ControllerContext() {
                HttpContext = new DefaultHttpContext() {
                    User = new ClaimsPrincipal(new ClaimsIdentity([
                        new Claim(ClaimTypes.NameIdentifier, "1")
                    ]))
                }
            };
            var userController = new UserController(
                userServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            ) {
                ControllerContext = controllerContext
            };

            // Act
            var result = await userController.DeleteUserAsync();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Test_Delete_User_Success_Return_Ok_Result()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();

            userServiceMock.Setup(x => x.DeleteUserAsync(It.IsAny<int>())).ReturnsAsync(true);

            var autoMapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger<UserController>>();
            var controllerContext = new ControllerContext() {
                HttpContext = new DefaultHttpContext() {
                    User = new ClaimsPrincipal(new ClaimsIdentity([
                        new Claim(ClaimTypes.NameIdentifier, "1")
                    ]))
                }
            };
            var userController = new UserController(
                userServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            ) {
                ControllerContext = controllerContext
            };

            // Act
            var result = await userController.DeleteUserAsync();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Test_Get_User_Not_Found_Return_NotFound_Result()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();

            userServiceMock.Setup(x => x.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync((User)null);

            var autoMapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger<UserController>>();
            var controllerContext = new ControllerContext() {
                HttpContext = new DefaultHttpContext() {
                    User = new ClaimsPrincipal(new ClaimsIdentity([
                        new Claim(ClaimTypes.NameIdentifier, "1")
                    ]))
                }
            };
            var userController = new UserController(
                userServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            ) {
                ControllerContext = controllerContext
            };

            // Act
            var result = await userController.GetUserAsync();

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Test_Get_User_Success_Return_Ok_Result()
        {
            // Arrange
            var user = CreateUser();
            var userServiceMock = new Mock<IUserService>();

            userServiceMock.Setup(x => x.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync(user);

            var autoMapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger<UserController>>();
            var controllerContext = new ControllerContext() {
                HttpContext = new DefaultHttpContext() {
                    User = new ClaimsPrincipal(new ClaimsIdentity([
                        new Claim(ClaimTypes.NameIdentifier, "1")
                    ]))
                }
            };
            var userController = new UserController(
                userServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            ) {
                ControllerContext = controllerContext
            };

            // Act
            var result = await userController.GetUserAsync();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Test_Logout_Faile_Return_BadRequest_Result()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();

            userServiceMock.Setup(x => x.DeleteRefreshTokenAsync(It.IsAny<int>())).ReturnsAsync(false);

            var autoMapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger<UserController>>();
            var controllerContext = new ControllerContext() {
                HttpContext = new DefaultHttpContext() {
                    User = new ClaimsPrincipal(new ClaimsIdentity([
                        new Claim(ClaimTypes.NameIdentifier, "1")
                    ]))
                }
            };
            var userController = new UserController(
                userServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            ) {
                ControllerContext = controllerContext
            };

            // Act
            var result = await userController.LogoutAsync();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        public async Task Test_Logout_Success_Return_Ok_Result()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();

            userServiceMock.Setup(x => x.DeleteRefreshTokenAsync(It.IsAny<int>())).ReturnsAsync(true);

            var autoMapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger<UserController>>();
            var controllerContext = new ControllerContext() {
                HttpContext = new DefaultHttpContext() {
                    User = new ClaimsPrincipal(new ClaimsIdentity([
                        new Claim(ClaimTypes.NameIdentifier, "1")
                    ]))
                }
            };
            var userController = new UserController(
                userServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            ) {
                ControllerContext = controllerContext
            };

            // Act
            var result = await userController.LogoutAsync();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Test_Recover_User_Fail_Return_BadRequest_Result()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();

            userServiceMock.Setup(x => x.RecoverUserAsync(It.IsAny<int>())).ReturnsAsync(false);

            var autoMapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger<UserController>>();
            var controllerContext = new ControllerContext() {
                HttpContext = new DefaultHttpContext() {
                    User = new ClaimsPrincipal(new ClaimsIdentity([
                        new Claim(ClaimTypes.NameIdentifier, "1")
                    ]))
                }
            };
            var userController = new UserController(
                userServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            ) {
                ControllerContext = controllerContext
            };

            // Act
            var result = await userController.RecoverUserAsync();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Test_Recover_User_Success_Return_Ok_Result()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();

            userServiceMock.Setup(x => x.RecoverUserAsync(It.IsAny<int>())).ReturnsAsync(true);

            var autoMapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger<UserController>>();
            var controllerContext = new ControllerContext() {
                HttpContext = new DefaultHttpContext() {
                    User = new ClaimsPrincipal(new ClaimsIdentity([
                        new Claim(ClaimTypes.NameIdentifier, "1")
                    ]))
                }
            };
            var userController = new UserController(
                userServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            ) {
                ControllerContext = controllerContext
            };

            // Act
            var result = await userController.RecoverUserAsync();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Test_Update_User_Validation_Fail_Return_BadRequest_Result()
        {
            // Arrange
            var userUpdateRequestDto = new UserUpdateRequestDto {
                Id = 1,
                Username = ""
            };
            var userServiceMock = new Mock<IUserService>();
            var autoMapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger<UserController>>();
            var userController = new UserController(
                userServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            );

            userController.ModelState.AddModelError("FirstName", "Username is required");
            userController.ModelState.AddModelError("LastName", "Username is required");

            // Act
            var result = await userController.UpdateUserAsync(userUpdateRequestDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Test_Update_User_Success_Return_Ok_Result()
        {
            var userUpdateRequestDto = new UserUpdateRequestDto();
            var user = CreateUser();
            var userServiceMock = new Mock<IUserService>();

            userServiceMock.Setup(x => x.UpdateUserAsync(It.IsAny<User>())).ReturnsAsync(user);

            var autoMapperMock = new Mock<IMapper>();

            autoMapperMock.Setup(x => x.Map<User>(It.IsAny<UserUpdateRequestDto>())).Returns(user);
            autoMapperMock.Setup(x => x.Map<UserUpdateResponseDto>(It.IsAny<User>())).Returns(It.IsAny<UserUpdateResponseDto>());

            var loggerMock = new Mock<ILogger<UserController>>();
            var userController = new UserController(
                userServiceMock.Object,
                autoMapperMock.Object,
                loggerMock.Object
            );

            // Act
            var result = await userController.UpdateUserAsync(userUpdateRequestDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
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
