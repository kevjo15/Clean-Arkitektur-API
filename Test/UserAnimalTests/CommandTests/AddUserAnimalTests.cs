using Application.Commands.UserAnimal.AddUserAnimal;
using Domain.Models;
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
    public class AddUserAnimalTests
    {
        private Mock<IUserAnimalRepository> _repositoryMock;
        private AddUserAnimalCommandHandler _handler;
        private Mock<ILogger<AddUserAnimalCommandHandler>> _loggerMock;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = new Mock<IUserAnimalRepository>();
            _loggerMock = new Mock<ILogger<AddUserAnimalCommandHandler>>();
            _handler = new AddUserAnimalCommandHandler(_repositoryMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task Handle_ValidRequest_AddsUserAnimal()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var animalModelId = Guid.NewGuid();
            var command = new AddUserAnimalCommand { UserId = userId, AnimalModelId = animalModelId };

            var userAnimal = new UserAnimal
            {
                // Tilldela värden till UserAnimal, inklusive userId och animalModelId
            };

            _repositoryMock.Setup(repo => repo.AddUserAnimalAsync(userId, animalModelId)).ReturnsAsync(userAnimal);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.UserId, Is.EqualTo(userId));
            // Kontrollera andra relevanta delar av resultaten
        }
        [Test]
        public void Handle_AddThrowsException_ThrowsException()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var animalModelId = Guid.NewGuid();
            var command = new AddUserAnimalCommand { UserId = userId, AnimalModelId = animalModelId };

            _repositoryMock.Setup(repo => repo.AddUserAnimalAsync(userId, animalModelId))
                           .ThrowsAsync(new InvalidOperationException("Test Exception"));

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
        }


    }
}