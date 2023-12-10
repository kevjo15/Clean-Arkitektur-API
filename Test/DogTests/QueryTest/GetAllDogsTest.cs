using Application.Queries.Dogs;
using Application.Queries.Dogs.GetAll;
using Domain.Models;
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

            // Act
            var result = await _handler.Handle(new GetAllDogsQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<List<Dog>>(result);

            //var dogs = (List<Dog>)result;
            //Assert.That(dogs.Count, Is.EqualTo(_AppDbContextMock.Object.Dogs.Count()));
        }
    }
}
