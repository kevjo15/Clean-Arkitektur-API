using Application.Queries.Dogs;
using Application.Queries.Dogs.GetAll;
using Domain.Models;
using Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.DogTests.QueryTest
{
    [TestFixture]
    public class GetAllDogsTest
    {
        private GetAllDogsQueryHandler _handler;
        private MockDatabase _mockDatabase;
        private MockDatabase? _temporaryDatabase;

        [SetUp]
        public void SetUp()
        {
            // Initialize the handler and mock database before each test
            _mockDatabase = new MockDatabase();

            _handler = new GetAllDogsQueryHandler(_mockDatabase);
        }
        [Test]
        public async Task GetAllDogs_ShouldReturnListOfDog()
        {
            // Arrange

            // Act
            var result = await _handler.Handle(new GetAllDogsQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<List<Dog>>(result);

            var dogs = (List<Dog>)result;
            Assert.That(dogs.Count, Is.EqualTo(_mockDatabase.Dogs.Count));
        }
        //[Test]
        //public async Task Handle_ReturnsEmptyListWhenNoDogs()
        //{
        //    // Arrange
        //    _temporaryDatabase = new MockDatabase();
        //    _temporaryDatabase.Dogs.Clear(); // Rensa hundlistan för att simulera att det inte finns några hundar

        //    // Act
        //    var result = await _handler.Handle(new GetAllDogsQuery(), CancellationToken.None);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.IsInstanceOf<List<Dog>>(result);

        //    var dogs = (List<Dog>)result;
        //    Assert.That(dogs.Count, Is.EqualTo(0));
        //}

    }
}
