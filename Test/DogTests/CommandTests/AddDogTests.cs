using Application.Commands.Dogs;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace Test.DogTests.CommandTest
{
    [TestFixture]
    public class AddDogTests
    {
        private Mock<IDogRepository> _dogRepositoryMock;
        private AddDogCommandHandler _handler;
        private Mock<ILogger<AddDogCommandHandler>> _loggerMock;

        [SetUp]
        public void SetUp()
        {
            _dogRepositoryMock = new Mock<IDogRepository>();
            _loggerMock = new Mock<ILogger<AddDogCommandHandler>>();
            _handler = new AddDogCommandHandler(_dogRepositoryMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task Handle_ValidRequest_AddsDog()
        {
            // Arrange
            var newDogDto = new DogDto { Name = "Buddy", Breed = "Labrador", Weight = 30 };
            var command = new AddDogCommand(newDogDto);

            var createdDog = new Dog
            {
                Id = Guid.NewGuid(), // Låt Id genereras dynamiskt
                Name = newDogDto.Name,
                Breed = newDogDto.Breed,
                Weight = newDogDto.Weight
            };

            _dogRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Dog>())).ReturnsAsync(createdDog);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(newDogDto.Name, result.Name);
            Assert.AreEqual(newDogDto.Breed, result.Breed);
            Assert.AreEqual(newDogDto.Weight, result.Weight);
        }

        [Test]
        public void Handle_AddThrowsException_ThrowsException()
        {
            // Arrange
            var newDogDto = new DogDto { Name = "Buddy", Breed = "Labrador", Weight = 30 };
            var command = new AddDogCommand(newDogDto);

            _dogRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Dog>()))
                              .ThrowsAsync(new InvalidOperationException("Test Exception"));

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
        }


    }
}