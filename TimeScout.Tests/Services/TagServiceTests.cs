using System.Threading.Tasks;
using Moq;
using TimeScout.API.Models;
using TimeScout.API.Repository;
using TimeScout.API.Services;

namespace TimeScout.Tests.Services
{
    public class TagServiceTests
    {
        [Fact]
        public async Task Test_Create_Tag_Failed_Should_Return_Null()
        {
            // Arrange
            var newTag = new Tag {
                Name = "Test",
                UserId = 0,
            };
            var tagRepositoryMock = new Mock<ITagRepository>();
            var tagService = new TagService(tagRepositoryMock.Object);

            // Act
            var actual = await tagService.CreateTagAsync(newTag);

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public async Task Test_Create_Tag_Success_Should_Return_Tag()
        {
            // Arrange
            var newTag = new Tag {
                Name = "Test",
                Color = "white",
                UserId = 1
            };
            var tagRepositoryMock = new Mock<ITagRepository>();

            tagRepositoryMock.Setup(x => x.AddAsync(newTag)).Verifiable();

            var tagService = new TagService(tagRepositoryMock.Object);

            // Act
            var actual = await tagService.CreateTagAsync(newTag);

            // Assert
            Assert.IsType<Tag>(actual);
        }

        [Fact]
        public async Task Test_Delete_Tag_Failed_Should_Return_False()
        {
            // Arrange
            var tag = new Tag {
                Id = 1,
                Name = "Test",
                Color = "white",
                UserId = 1
            };
            var tagRepositoryMock = new Mock<ITagRepository>();

            tagRepositoryMock.Setup(x => x.FindAsync(1)).ReturnsAsync((Tag)null);

            var tagService = new TagService(tagRepositoryMock.Object);

            // Act
            var actual = await tagService.DeleteTagAsync(1, 1);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public async Task Test_Delete_Tag_Success_Should_Return_True()
        {
            // Arrange
            var tag = new Tag {
                Id = 1,
                Name = "Test",
                Color = "white",
                UserId = 1
            };
            var tagRepositoryMock = new Mock<ITagRepository>();

            tagRepositoryMock.Setup(x => x.FindAsync(1)).ReturnsAsync(tag);
            tagRepositoryMock.Setup(x => x.RemoveAsync(tag)).Verifiable();

            var tagService = new TagService(tagRepositoryMock.Object);

            // Act
            var actual = await tagService.DeleteTagAsync(1, 1);

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public async Task Test_Get_All_Tags_Should_Return_Tags()
        {
            // Arrange
            var expected = new List<Tag> {
                new() {
                    Id = 1,
                    Name = "Test",
                    Color = "white",
                    UserId = 1
                },
                new() {
                    Id = 2,
                    Name = "Try",
                    Color = "black",
                    UserId = 1
                }
            };
            var tagRepositoryMock = new Mock<ITagRepository>();

            tagRepositoryMock.Setup(x => x.GetAllTagsAsync(1)).ReturnsAsync(expected);

            var tagService = new TagService(tagRepositoryMock.Object);

            // Act
            var actual = await tagService.GetAllTagsAsync(1);

            // Assert
            Assert.Same(expected, actual);
        }

        [Fact]
        public async Task Test_Get_Tag_By_Id_Not_Found_Should_Return_Null()
        {
            // Arrange
            var tagRepositoryMock = new Mock<ITagRepository>();

            tagRepositoryMock.Setup(x => x.GetTagByIdAsync(1, 1)).ReturnsAsync((Tag)null);

            var tagService = new TagService(tagRepositoryMock.Object);

            // Act
            var actual = await tagService.GetTagByIdAsync(1, 1);

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public async Task Test_Get_Tag_By_Id_Success_Should_Return_Tag()
        {
            // Arrange
            var expected = new Tag {
                Id = 1,
                Name = "Test",
                Color = "white",
                UserId = 1
            };
            var tagRepositoryMock = new Mock<ITagRepository>();

            tagRepositoryMock.Setup(x => x.GetTagByIdAsync(1, 1)).ReturnsAsync(expected);

            var tagService = new TagService(tagRepositoryMock.Object);

            // Act
            var actual = await tagService.GetTagByIdAsync(1, 1);

            // Assert
            Assert.Same(expected, actual);
        }

        [Fact]
        public async Task Test_Update_Tag_Tag_To_Update_Null_Should_Return_Null()
        {
            // Arrange
            var updateTag = new Tag {
                Id = 1,
                Name = "Test",
                Color = "white",
                UserId = 1
            };
            var tagRepositoryMock = new Mock<ITagRepository>();

            tagRepositoryMock.Setup(x => x.FindAsync(1)).ReturnsAsync((Tag)null);

            var tagService = new TagService(tagRepositoryMock.Object);

            // Act
            var actual = await tagService.UpdateTagAsync(updateTag);

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public async Task Test_Update_Tag_User_Id_Invalid_Should_Return_Null()
        {
            // Arrange
            var updateTag = new Tag {
                Id = 1,
                Name = "Test",
                Color = "white",
                UserId = 1
            };
            var tagToUpdate = new Tag {
                Id = 1,
                Name = "Tag",
                Color = "red",
                UserId = 2
            };
            var tagRepositoryMock = new Mock<ITagRepository>();

            tagRepositoryMock.Setup(x => x.FindAsync(1)).ReturnsAsync(tagToUpdate);

            var tagService = new TagService(tagRepositoryMock.Object);

            // Act
            var actual = await tagService.UpdateTagAsync(updateTag);

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public async Task Test_Update_Tag_Success_Should_Return_Tag()
        {
            // Arrange
            var expected = new Tag {
                Id = 1,
                Name = "New",
                Color = "blue",
                UserId = 1
            };
            var tagToUpdate = new Tag {
                Id = 1,
                Name = "Old",
                Color = "white",
                UserId = 1
            };
            var tagRepositoryMock = new Mock<ITagRepository>();

            tagRepositoryMock.Setup(x => x.FindAsync(1)).ReturnsAsync(tagToUpdate);
            tagRepositoryMock.Setup(x => x.UpdateAsync(tagToUpdate)).Verifiable();

            var tagService = new TagService(tagRepositoryMock.Object);

            // Act
            var actual = await tagService.UpdateTagAsync(expected);

            // Assert
            Assert.Equivalent(expected, actual);
        }
    }
}
