using Application.Queries.Dogs.GetDogsByBreedAndWeight;
using Domain.Models;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.DogTests.QueryTest
{
    [TestFixture]
    public class GetDogsByBreedAndWeightTests
    {
        private Mock<IDogRepository> _dogRepositoryMock;
        private GetDogsByBreedAndWeightQueryHandler _handler;
        private Mock<ILogger<GetDogsByBreedAndWeightQueryHandler>> _loggerMock;

        [SetUp]
        public void SetUp()
        {
            _dogRepositoryMock = new Mock<IDogRepository>();
            _loggerMock = new Mock<ILogger<GetDogsByBreedAndWeightQueryHandler>>();
            _handler = new GetDogsByBreedAndWeightQueryHandler(_dogRepositoryMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task Handle_WithMatchingCriteria_ReturnsDogs()
        {
            // Arrange
            var breed = "Labrador";
            var weight = 30;
            var dogs = new List<Dog> { new Dog { Id = Guid.NewGuid(), Breed = breed, Weight = weight } };
            _dogRepositoryMock.Setup(repo => repo.GetByBreedAndWeightAsync(breed, weight)).ReturnsAsync(dogs);

            var query = new GetDogsByBreedAndWeightQuery(breed, weight);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotEmpty(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(breed, result.First().Breed);
            Assert.AreEqual(weight, result.First().Weight);
        }

        [Test]
        public async Task Handle_NoMatchingCriteria_ReturnsEmptyList()
        {
            // Arrange
            var breed = "Poodle";
            var weight = 10;
            _dogRepositoryMock.Setup(repo => repo.GetByBreedAndWeightAsync(breed, weight)).ReturnsAsync(new List<Dog>());

            var query = new GetDogsByBreedAndWeightQuery(breed, weight);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsEmpty(result);
        }
    }
}