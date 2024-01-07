using Application.Queries.Dogs.GetById;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace Test.DogTests.QueryTest
{
    [TestFixture]
    public class GetDogByIdTests
    {
        private GetDogByIdQueryHandler _handler;
        private Mock<IDogRepository> _dogRepositoryMock;
        private Mock<ILogger<GetDogByIdQueryHandler>> _loggerMock;

        [SetUp]
        public void SetUp()
        {
            _dogRepositoryMock = new Mock<IDogRepository>();
            _loggerMock = new Mock<ILogger<GetDogByIdQueryHandler>>();
            _handler = new GetDogByIdQueryHandler(_dogRepositoryMock.Object, _loggerMock.Object);
        }

        // GetDogById
        [Test]
        public async Task Handle_ValidId_ReturnsCorrectDog()
        {
            // Arrange
            var dogId = new Guid("12345678-1234-5678-1234-567812345678");
            var dog = new Dog { Id = dogId, Name = "Rex" };
            _dogRepositoryMock.Setup(repo => repo.GetByIdAsync(dogId)).ReturnsAsync(dog);

            var query = new GetDogByIdQuery(dogId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(dogId));
            Assert.That(result.Name, Is.EqualTo("Rex"));
        }
        // GetDogById
        [Test]
        public async Task Handle_InvalidId_ReturnsNull()
        {
            // Arrange
            var invalidDogId = Guid.NewGuid();
            _dogRepositoryMock.Setup(repo => repo.GetByIdAsync(invalidDogId)).ReturnsAsync((Dog)null);

            var query = new GetDogByIdQuery(invalidDogId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNull(result);
        }
    }
}
