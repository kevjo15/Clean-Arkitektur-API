using Application.Commands.Dogs.UpdateDog;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;


namespace Test.DogTests.CommandTest
{
    [TestFixture]
    public class UpdateDogTests
    {
        private Mock<IDogRepository> _dogRepositoryMock;
        private UpdateDogByIdCommandHandler _handler;
        private Mock<ILogger<UpdateDogByIdCommandHandler>> _loggerMock;

        [SetUp]
        public void SetUp()
        {
            _dogRepositoryMock = new Mock<IDogRepository>();
            _loggerMock = new Mock<ILogger<UpdateDogByIdCommandHandler>>();
            _handler = new UpdateDogByIdCommandHandler(_dogRepositoryMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task Handle_ValidRequest_UpdatesDog()
        {
            // Arrange
            var dogId = Guid.NewGuid();
            var existingDog = new Dog { Id = dogId, Name = "OldName", Breed = "OldBreed", Weight = 10 };
            var updatedDogDto = new DogDto { Name = "NewName", Breed = "NewBreed", Weight = 20 };
            var command = new UpdateDogByIdCommand(updatedDogDto, dogId);

            _dogRepositoryMock.Setup(repo => repo.GetByIdAsync(dogId)).ReturnsAsync(existingDog);
            _dogRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Dog>())).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updatedDogDto.Name, result.Name);
            Assert.AreEqual(updatedDogDto.Breed, result.Breed);
            Assert.AreEqual(updatedDogDto.Weight, result.Weight);
        }
        [Test]
        public void Handle_UpdateThrowsException_ThrowsException()
        {
            // Arrange
            var dogId = Guid.NewGuid();
            var existingDog = new Dog { Id = dogId, Name = "OldName", Breed = "OldBreed", Weight = 10 };
            var updatedDogDto = new DogDto { Name = "NewName", Breed = "NewBreed", Weight = 20 };
            var command = new UpdateDogByIdCommand(updatedDogDto, dogId);

            _dogRepositoryMock.Setup(repo => repo.GetByIdAsync(dogId)).ReturnsAsync(existingDog);
            _dogRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Dog>())).ThrowsAsync(new InvalidOperationException("Test Exception"));

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
        }


    }
}