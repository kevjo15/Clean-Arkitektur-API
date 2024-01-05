using API.Controllers.DogsController;
using Application.Commands.Birds.DeleteBird;
using Application.Commands.Dogs.DeleteDog;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;


namespace Test.DogTests.CommandTest
{
    [TestFixture]
    public class DeleteDogByIdTests
    {
        private Mock<IDogRepository> _dogRepositoryMock;
        private DeleteDogByIdCommandHandler _handler;
        private Mock<ILogger<DeleteDogByIdCommandHandler>> _loggerMock;

        [SetUp]
        public void SetUp()
        {
            _dogRepositoryMock = new Mock<IDogRepository>();
            _loggerMock = new Mock<ILogger<DeleteDogByIdCommandHandler>>();
            _handler = new DeleteDogByIdCommandHandler(_dogRepositoryMock.Object, _loggerMock.Object);
        }


        [Test]
        public async Task Handle_ValidId_DeletesDog()
        {
            // Arrange
            var dogId = Guid.NewGuid();
            var dog = new Dog { Id = dogId, Name = "TestDog" };
            _dogRepositoryMock.Setup(repo => repo.GetByIdAsync(dogId)).ReturnsAsync(dog);
            _dogRepositoryMock.Setup(repo => repo.DeleteAsync(dogId)).Returns(Task.CompletedTask);

            var command = new DeleteDogByIdCommand(dogId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(dogId, result.Id);
            // Kontrollera andra relevanta delar av resultaten
        }

        [Test]
        public void Handle_DeleteThrowsException_ThrowsException()
        {
            // Arrange
            var dogId = Guid.NewGuid();
            _dogRepositoryMock.Setup(repo => repo.GetByIdAsync(dogId)).ReturnsAsync(new Dog { Id = dogId });
            _dogRepositoryMock.Setup(repo => repo.DeleteAsync(dogId)).ThrowsAsync(new InvalidOperationException("Test Exception"));

            var command = new DeleteDogByIdCommand(dogId);

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
        }

    }
}
