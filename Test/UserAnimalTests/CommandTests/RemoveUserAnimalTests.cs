using Application.Commands.UserAnimal.RemoveUserAnimal;
using Infrastructure.Database.Repositories.UserAnimalRepository;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.UserAnimalTests.CommandTest
{
    [TestFixture]
    public class RemoveUserAnimalTests
    {
        private Mock<IUserAnimalRepository> _userAnimalRepositoryMock;
        private RemoveUserAnimalCommandHandler _handler;
        private Mock<ILogger<RemoveUserAnimalCommandHandler>> _loggerMock;

        [SetUp]
        public void SetUp()
        {
            _userAnimalRepositoryMock = new Mock<IUserAnimalRepository>();
            _loggerMock = new Mock<ILogger<RemoveUserAnimalCommandHandler>>();
            _handler = new RemoveUserAnimalCommandHandler(_userAnimalRepositoryMock.Object, _loggerMock.Object);
        }
        [Test]
        public async Task Handle_ValidRequest_RemovesUserAnimal()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var animalId = Guid.NewGuid();
            var command = new RemoveUserAnimalCommand(userId, animalId);

            _userAnimalRepositoryMock.Setup(repo => repo.RemoveUserAnimalAsync(userId, animalId))
                                     .Returns(Task.CompletedTask);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _userAnimalRepositoryMock.Verify(repo => repo.RemoveUserAnimalAsync(userId, animalId), Times.Once);
        }
        [Test]
        public async Task Handle_RemoveFails_ReturnsFalse()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var animalId = Guid.NewGuid();
            var command = new RemoveUserAnimalCommand(userId, animalId);

            _userAnimalRepositoryMock.Setup(repo => repo.RemoveUserAnimalAsync(userId, animalId))
                                     .ThrowsAsync(new InvalidOperationException("Test Exception"));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result);
            _loggerMock.Verify(
              logger => logger.Log(
              LogLevel.Error,
               It.IsAny<EventId>(),
               It.IsAny<It.IsAnyType>(),
               It.IsAny<Exception>(),
               (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
          Times.AtLeastOnce);
        }


    }
}
