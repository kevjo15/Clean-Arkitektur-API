using Application.Queries.Cats.GetCatsByBreedAndWeight;
using Domain.Models;
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
    public class GetCatsByBreedAndWeightTests
    {
        private Mock<ICatRepository> _catRepositoryMock;
        private GetCatsByBreedAndWeightQueryHandler _handler;
        private Mock<ILogger<GetCatsByBreedAndWeightQueryHandler>> _loggerMock;

        [SetUp]
        public void SetUp()
        {
            _catRepositoryMock = new Mock<ICatRepository>();
            _loggerMock = new Mock<ILogger<GetCatsByBreedAndWeightQueryHandler>>();
            _handler = new GetCatsByBreedAndWeightQueryHandler(_catRepositoryMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task Handle_WithMatchingCriteria_ReturnsCats()
        {
            // Arrange
            var breed = "Siamese";
            var weight = 5;
            var cats = new List<Cat> { new Cat { Id = Guid.NewGuid(), Breed = breed, Weight = weight } };
            _catRepositoryMock.Setup(repo => repo.GetByBreedAndWeightAsync(breed, weight)).ReturnsAsync(cats);

            var query = new GetCatsByBreedAndWeightQuery(breed, weight);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotEmpty(result);
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Breed, Is.EqualTo(breed));
            Assert.That(result.First().Weight, Is.EqualTo(weight));
        }

        [Test]
        public async Task Handle_NoMatchingCriteria_ReturnsEmptyList()
        {
            // Arrange
            var breed = "Bengal";
            var weight = 10;
            _catRepositoryMock.Setup(repo => repo.GetByBreedAndWeightAsync(breed, weight)).ReturnsAsync(new List<Cat>());

            var query = new GetCatsByBreedAndWeightQuery(breed, weight);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsEmpty(result);
        }
    }
}
