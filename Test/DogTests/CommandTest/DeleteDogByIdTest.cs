﻿using API.Controllers.DogsController;
using Application.Commands.Birds.DeleteBird;
using Application.Commands.Dogs.DeleteDog;
using Infrastructure.Database;


namespace Test.DogTests.CommandTest
{
    [TestFixture]
    public class DeleteDogByIdTest
    {
        private DeleteDogByIdCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void Setup()
        {
            _mockDatabase = new MockDatabase();
            _handler = new DeleteDogByIdCommandHandler(_mockDatabase);
        }

        [Test]
        public async Task DeleteDogById_ShouldRemoveDogIfExistingDogIsDeleted()
        {
            // Arrange
            var existingDogId = new Guid("12345678-1234-5678-1234-567812345679");
            var deleteCommand = new DeleteDogByIdCommand(existingDogId);

            // Act
            var result = await _handler.Handle(deleteCommand, new CancellationToken());

            // Assert
            var dogExistsAfterDeletion = _mockDatabase.Dogs.Any(d => d.Id == existingDogId);
            Assert.IsFalse(dogExistsAfterDeletion, "Dog should be deleted from the database");


        }

    }
}
