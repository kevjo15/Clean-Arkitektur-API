using Application.Queries.Cats.GetById;
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
    public class GetCatByIdTests
    {
        private Mock<ICatRepository> _catRepositoryMock;
        private GetCatByIdQueryHandler _handler;
        private Mock<ILogger<GetCatByIdQueryHandler>> _loggerMock;

        [SetUp]
        public void SetUp()
        {
            _catRepositoryMock = new Mock<ICatRepository>();
            _loggerMock = new Mock<ILogger<GetCatByIdQueryHandler>>();
            _handler = new GetCatByIdQueryHandler(_catRepositoryMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task Handle_ValidId_ReturnsCorrectCat()
        {
            // Arrange
            var catId = Guid.NewGuid();
            var cat = new Cat { Id = catId, Name = "Whiskers" };
            _catRepositoryMock.Setup(repo => repo.GetByIdAsync(catId)).ReturnsAsync(cat);

            var query = new GetCatByIdQuery(catId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(catId));
            Assert.That(result.Name, Is.EqualTo("Whiskers"));
        }

        [Test]
        public async Task Handle_InvalidId_ReturnsNull()
        {
            // Arrange
            var invalidCatId = Guid.NewGuid();
            _catRepositoryMock.Setup(repo => repo.GetByIdAsync(invalidCatId)).ReturnsAsync((Cat)null);

            var query = new GetCatByIdQuery(invalidCatId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNull(result);
        }
    }
}
