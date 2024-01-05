using Application.Commands.UserAnimal.UpdateUserAnimal;
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
    public class UpdateUserAnimalTests
    {
        private Mock<IUserAnimalRepository> _userAnimalRepositoryMock;
        private UpdateUserAnimalCommandHandler _handler;
        private Mock<ILogger<UpdateUserAnimalCommandHandler>> _loggerMock;

        [SetUp]
        public void SetUp()
        {
            _userAnimalRepositoryMock = new Mock<IUserAnimalRepository>();
            _loggerMock = new Mock<ILogger<UpdateUserAnimalCommandHandler>>();
            _handler = new UpdateUserAnimalCommandHandler(_userAnimalRepositoryMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task Handle_ValidRequest_UpdatesUserAnimal()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var currentAnimalId = Guid.NewGuid();
            var newAnimalId = Guid.NewGuid();
            var command = new UpdateUserAnimalCommand(userId, currentAnimalId, newAnimalId);

            _userAnimalRepositoryMock.Setup(repo => repo.UpdateUserAnimalAsync(userId, currentAnimalId, newAnimalId)).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result);
            _userAnimalRepositoryMock.Verify(repo => repo.UpdateUserAnimalAsync(userId, currentAnimalId, newAnimalId), Times.Once);
        }
        [Test]
        public async Task Handle_UpdateFails_ReturnsFalse()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var currentAnimalId = Guid.NewGuid();
            var newAnimalId = Guid.NewGuid();
            var command = new UpdateUserAnimalCommand(userId, currentAnimalId, newAnimalId);

            _userAnimalRepositoryMock.Setup(repo => repo.UpdateUserAnimalAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>()))
             .ThrowsAsync(new InvalidOperationException("Test Exception"));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result); // Kontrollera att svaret är false
            _userAnimalRepositoryMock.Verify(repo => repo.UpdateUserAnimalAsync(userId, currentAnimalId, newAnimalId), Times.Once);
        }






    }
}
