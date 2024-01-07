using Application.Commands.Birds.UpdateBird;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Database.Repositories.Birds;
using Microsoft.Extensions.Logging;
using Moq;

namespace Test.BirdTests.CommandTest
{
    [TestFixture]
    public class UpdateBirdTests
    {
        private Mock<IBirdRepository> _birdRepositoryMock;
        private UpdateBirdByIdCommandHandler _handler;
        private Mock<ILogger<UpdateBirdByIdCommandHandler>> _loggerMock;

        [SetUp]
        public void SetUp()
        {
            _birdRepositoryMock = new Mock<IBirdRepository>();
            _loggerMock = new Mock<ILogger<UpdateBirdByIdCommandHandler>>();
            _handler = new UpdateBirdByIdCommandHandler(_birdRepositoryMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task Handle_ValidRequest_UpdatesBird()
        {
            // Arrange
            var birdId = Guid.NewGuid();
            var bird = new Bird { Id = birdId, Name = "OldName", CanFly = true, Color = "Red" };
            var updatedBird = new BirdDto { Name = "NewName", CanFly = false, Color = "Blue" };
            var command = new UpdateBirdByIdCommand(updatedBird, birdId);

            _birdRepositoryMock.Setup(repo => repo.GetByIdAsync(birdId)).ReturnsAsync(bird);
            _birdRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Bird>())).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Name, Is.EqualTo(updatedBird.Name));
            Assert.That(result.CanFly, Is.EqualTo(updatedBird.CanFly));
            Assert.That(result.Color, Is.EqualTo(updatedBird.Color));
        }
        [Test]
        public void Handle_UpdateThrowsException_ThrowsException()
        {
            // Arrange
            var birdId = Guid.NewGuid();
            var existingBird = new Bird { Id = birdId, Name = "OldName", CanFly = true, Color = "Red" };
            var updatedBirdDto = new BirdDto { Name = "NewName", CanFly = false, Color = "Blue" };
            var command = new UpdateBirdByIdCommand(updatedBirdDto, birdId);

            _birdRepositoryMock.Setup(repo => repo.GetByIdAsync(birdId)).ReturnsAsync(existingBird);
            _birdRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Bird>())).ThrowsAsync(new InvalidOperationException("Test Exception"));

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
        }

    }
}
