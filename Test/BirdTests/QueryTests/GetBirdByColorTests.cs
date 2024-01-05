using Application.Queries.Birds.GetByColor;
using Domain.Models;
using Infrastructure.Database.Repositories.Birds;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.BirdTests.QueryTest
{
    [TestFixture]
    public class GetBirdByColorTests
    {
        private Mock<IBirdRepository> _birdRepositoryMock;
        private Mock<ILogger<GetBirdByColorQueryHandler>> _loggerMock;
        private GetBirdByColorQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _birdRepositoryMock = new Mock<IBirdRepository>();
            _loggerMock = new Mock<ILogger<GetBirdByColorQueryHandler>>();
            _handler = new GetBirdByColorQueryHandler(_birdRepositoryMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task Handle_BirdsFound_ReturnsBirds()
        {
            // Arrange
            var color = "Red";
            var birds = new List<Bird> { new Bird { Name = "Cardinal", Color = "Red" } };
            _birdRepositoryMock.Setup(repo => repo.GetBirdByColorAsync(color)).ReturnsAsync(birds);

            var query = new GetBirdByColorQuery(color);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
            Assert.AreEqual("Cardinal", result[0].Name);
            _birdRepositoryMock.Verify(repo => repo.GetBirdByColorAsync(color), Times.Once);
        }

        [Test]
        public async Task Handle_NoBirdsFound_ReturnsEmptyList()
        {
            // Arrange
            var color = "Blue";
            _birdRepositoryMock.Setup(repo => repo.GetBirdByColorAsync(color)).ReturnsAsync(new List<Bird>());

            var query = new GetBirdByColorQuery(color);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
            _birdRepositoryMock.Verify(repo => repo.GetBirdByColorAsync(color), Times.Once);
        }
    }
}