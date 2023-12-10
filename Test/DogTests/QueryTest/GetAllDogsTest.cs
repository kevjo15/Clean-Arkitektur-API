using Application.Queries.Dogs;
using Application.Queries.Dogs.GetAll;
using Domain.Models;
using FluentAssertions;
using Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
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
        private RealDatabase _RealDatabase;
        //private AppDbContext _AppDbContext;
        private Mock<AppDbContext> _AppDbContextMock;
        private Mock<DbSet<Dog>> _DbSetMock;
        [SetUp]
        public void SetUp()
        {
            // Initialize the handler and mock database before each test
            //_RealDatabase = new RealDatabase();
            _AppDbContextMock = new Mock<AppDbContext>();
            _DbSetMock = new Mock<DbSet<Dog>>();

            

            _handler = new GetAllDogsQueryHandler(_AppDbContextMock.Object);
        }
        [Test]
        public async Task GetAllDogs_ShouldReturnListOfDog()
        {
            // Arrange
            var fakeDogs = new List<Dog>
        {
            new Dog { Id = Guid.NewGuid(), Name = "Buddy" },
            new Dog { Id = Guid.NewGuid(), Name = "Charlie" },
            // Add more fake dogs as needed
        }.AsQueryable();

            _DbSetMock.As<IQueryable<Dog>>().Setup(m => m.Provider).Returns(fakeDogs.Provider);
            _DbSetMock.As<IQueryable<Dog>>().Setup(m => m.Expression).Returns(fakeDogs.Expression);
            _DbSetMock.As<IQueryable<Dog>>().Setup(m => m.ElementType).Returns(fakeDogs.ElementType);
            _DbSetMock.As<IQueryable<Dog>>().Setup(m => m.GetEnumerator()).Returns(() => fakeDogs.GetEnumerator());

            _AppDbContextMock.Setup(m => m.Dogs).Returns(_DbSetMock.Object);
            // Act
            var result = await _handler.Handle(new GetAllDogsQuery(), CancellationToken.None);

            // Assert
            //Assert.NotNull(result);
            //Assert.IsInstanceOf<List<Dog>>(result);

            result.Should().NotBeNull();
            result.Should().BeOfType<List<Dog>>();
            result.Should().HaveCount(fakeDogs.Count());

            //var dogs = (List<Dog>)result;
            //Assert.That(dogs.Count, Is.EqualTo(_AppDbContextMock.Object.Dogs.Count()));
        }
    }
}
