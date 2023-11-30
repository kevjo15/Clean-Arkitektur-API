using API.Controllers.BirdsController;
using API.Controllers.CatsController;
using Application.Commands.Birds.DeleteBird;
using Domain.Models;
using Infrastructure.Database;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.BirdTests.CommandTest
{
    [TestFixture]
    public class DeleteBirdByIdTest
    {
        private DeleteBirdByIdCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void Setup()
        {
            _mockDatabase = new MockDatabase();
            _handler = new DeleteBirdByIdCommandHandler(_mockDatabase);
        }

        [Test]
        public async Task DeleteBirdById_ShouldRemoveBirdIfExistingBirdIsDeleted()
        {
            // Arrange
            var existingBirdId = new Guid("12345678-1234-5678-1234-567812345612");
            var deleteCommand = new DeleteBirdByIdCommand(existingBirdId);

            // Act
            var result = await _handler.Handle(deleteCommand, new CancellationToken());

            // Assert
            var birdExistsAfterDeletion = _mockDatabase.Birds.Any(b => b.Id == existingBirdId);

            Assert.IsFalse(birdExistsAfterDeletion, "Bird should be deleted from the database");


        }

    }
}
