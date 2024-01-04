using Application.Commands.Birds.AddBird;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Database.Repositories.Birds;
using Microsoft.Extensions.Logging;
using Moq;

namespace Test.BirdTests.CommandTest
{
    [TestFixture]
    public class AddBirdTests
    {
        private Mock<IBirdRepository> _birdRepositoryMock;
        private AddBirdCommandHandler _handler;
        private Mock<ILogger<AddBirdCommandHandler>> _loggerMock;

        [SetUp]
        public void SetUp()
        {
            _birdRepositoryMock = new Mock<IBirdRepository>();
            _loggerMock = new Mock<ILogger<AddBirdCommandHandler>>();
            _handler = new AddBirdCommandHandler(_birdRepositoryMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task Handle_ValidRequest_AddsBird()
        {
            // Arrange
            var newBirdDto = new BirdDto { Name = "Sunny", Color = "Yellow", CanFly = true };
            var command = new AddBirdCommand(newBirdDto);

            var birdToAdd = new Bird { Name = newBirdDto.Name, Color = newBirdDto.Color, CanFly = newBirdDto.CanFly };

            _birdRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Bird>()))
                               .Returns(Task.FromResult(birdToAdd)); // Returnerar en Task<Bird> istället för bara Task

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(newBirdDto.Name, result.Name);
            Assert.AreEqual(newBirdDto.Color, result.Color);
            Assert.AreEqual(newBirdDto.CanFly, result.CanFly);
        }


        [Test]
        public void Handle_AddThrowsException_ThrowsException()
        {
            // Arrange
            var newBirdDto = new BirdDto { Name = "Sunny", Color = "Yellow", CanFly = true };
            var command = new AddBirdCommand(newBirdDto);

            // Setup mock to throw an exception when AddAsync is called
            _birdRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Bird>()))
                               .ThrowsAsync(new InvalidOperationException("Test Exception"));

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}

