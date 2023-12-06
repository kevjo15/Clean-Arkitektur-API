﻿using Application.Queries.Cats.GetAll;
using Domain.Models;
using Infrastructure.Database;
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
        private GetAllCatsQueryHandler _handler;
        private RealDatabase _RealDatabase;

        [SetUp]
        public void SetUp()
        {
            // Initialize the handler and mock database before each test
            _RealDatabase = new RealDatabase();
            _handler = new GetAllCatsQueryHandler(_RealDatabase);

        }
        [Test]
        public async Task GetAllCats_ShouldReturnListOfCats()
        {
            // Arrange

            // Act
            var result = await _handler.Handle(new GetAllCatsQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<List<Cat>>(result);

            var cats = (List<Cat>)result;
            Assert.That(cats.Count, Is.EqualTo(_RealDatabase.Cats.Count));
        }
    }
}
