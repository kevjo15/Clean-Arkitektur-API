using Application.Queries.Cats.GetAll;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Database.Repositories.Cats;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.CatTests.QueryTest
{
    [TestFixture]
    public class GetAllCatsTest
    {
        private Mock<ICatRepository> _catRepositoryMock;
        private Mock<ILogger<GetAllCatsQueryHandler>> _loggerMock;
        private GetAllCatsQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _catRepositoryMock = new Mock<ICatRepository>();
            _loggerMock = new Mock<ILogger<GetAllCatsQueryHandler>>();
            _handler = new GetAllCatsQueryHandler(_catRepositoryMock.Object, _loggerMock.Object);

        }
        [Test]
        public async Task GetAllCats_WhenCatsExist_ReturnsAllCats()
        {
            // Arrange
            var fakeCats = new List<Cat> { new Cat(), new Cat() };
            _catRepositoryMock.Setup(repo => repo.GetAllCatsAsync())
                              .ReturnsAsync(fakeCats);

            // Act
            var query = new GetAllCatsQuery();
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            _catRepositoryMock.Verify(repo => repo.GetAllCatsAsync(), Times.Once);
        }
        [Test]
        public async Task GetAllCats_WhenRepositoryThrowsException_ReturnsEmptyList()
        {
            // Arrange
            _catRepositoryMock.Setup(repo => repo.GetAllCatsAsync())
                              .ThrowsAsync(new Exception("Database error"));

            // Act
            var query = new GetAllCatsQuery();
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
