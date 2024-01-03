using Application.Queries.Birds.GetAll;
using Application.Queries.Cats.GetAll;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Database.Repositories.Birds;
using Microsoft.Extensions.Logging;
using Moq;

namespace Test.BirdTests.QueryTest
{
    [TestFixture]
    public class GetAllBirdsTest
    {
        private Mock<IBirdRepository> _birdRepositoryMock;
        private Mock<ILogger<GetAllBirdsQueryHandler>> _loggerMock;
        private GetAllBirdsQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _birdRepositoryMock = new Mock<IBirdRepository>();
            _loggerMock = new Mock<ILogger<GetAllBirdsQueryHandler>>();
            _handler = new GetAllBirdsQueryHandler(_birdRepositoryMock.Object, _loggerMock.Object);
        }
        [Test]
        public async Task GetAllBirds_WhenBirdsExist_ReturnsAllBirds()
        {
            // Arrange
            var fakeBirds = new List<Bird> { new Bird(), new Bird() };
            _birdRepositoryMock.Setup(repo => repo.GetAllBirdAsync())
                              .ReturnsAsync(fakeBirds);

            // Act
            var query = new GetAllBirdsQuery();
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            _birdRepositoryMock.Verify(repo => repo.GetAllBirdAsync(), Times.Once);
        }
        [Test]
        public async Task GetAllBirds_WhenRepositoryThrowsException_ReturnsEmptyList()
        {
            // Arrange
            _birdRepositoryMock.Setup(repo => repo.GetAllBirdAsync())
                              .ThrowsAsync(new Exception("Database error"));

            // Act
            var query = new GetAllBirdsQuery();
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
            _loggerMock.Verify(logger => logger.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
                Times.Once);
        }
    }
}
