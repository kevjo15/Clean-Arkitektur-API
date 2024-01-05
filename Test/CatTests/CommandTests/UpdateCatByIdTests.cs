using Application.Commands.Cats.UpdateCat;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Database.Repositories.Cats;
using Microsoft.Extensions.Logging;
using Moq;

namespace Test.CatTests.CommandTest
{
    public class UpdateCatByIdTests
    {
        private Mock<ICatRepository> _catRepositoryMock;
        private UpdateCatByIdCommandHandler _handler;
        private Mock<ILogger<UpdateCatByIdCommandHandler>> _loggerMock;

        [SetUp]
        public void SetUp()
        {
            _catRepositoryMock = new Mock<ICatRepository>();
            _loggerMock = new Mock<ILogger<UpdateCatByIdCommandHandler>>();
            _handler = new UpdateCatByIdCommandHandler(_catRepositoryMock.Object, _loggerMock.Object);
        }
        [Test]
        public async Task Handle_ValidRequest_UpdatesCat()
        {
            // Arrange
            var catId = Guid.NewGuid();
            var existingCat = new Cat { Id = catId, Name = "OldName", LikesToPlay = true };
            var updatedCatDto = new CatDto { Name = "NewName", LikesToPlay = false };
            var command = new UpdateCatByIdCommand(updatedCatDto ,catId);

            _catRepositoryMock.Setup(repo => repo.GetByIdAsync(catId)).ReturnsAsync(existingCat);
            _catRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Cat>())).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updatedCatDto.Name, result.Name);
            Assert.AreEqual(updatedCatDto.LikesToPlay, result.LikesToPlay);
        }
        [Test]
        public void Handle_UpdateThrowsException_ThrowsException()
        {
            // Arrange
            var catId = Guid.NewGuid();
            var existingCat = new Cat { Id = catId, Name = "OldName", LikesToPlay = true };
            var updatedCatDto = new CatDto { Name = "NewName", LikesToPlay = false };
            var command = new UpdateCatByIdCommand(updatedCatDto, catId);

            _catRepositoryMock.Setup(repo => repo.GetByIdAsync(catId)).ReturnsAsync(existingCat);
            _catRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Cat>())).ThrowsAsync(new InvalidOperationException("Test Exception"));

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
        }

    }
}
