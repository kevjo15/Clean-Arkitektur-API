using Application.Commands.Dogs.UpdateDog;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Database;


namespace Test.DogTests.CommandTest
{
    [TestFixture]
    public class UpdateDogTests
    {
        private UpdateDogByIdCommandHandler _handler;
        private RealDatabase _RealDatabase;

        [SetUp]
        public void Setup()
        {
            _RealDatabase = new RealDatabase();
            _handler = new UpdateDogByIdCommandHandler(_RealDatabase);
        }

        [Test]
        public async Task Handle_UpdatesDogInDatabase()
        {
            // Arrange
            var dogId = new Guid("12345678-1234-5678-1234-567812345615");
            var dogToUpdate = new DogDto { Name = "UpdatedDogName" };
            //skapar en instans av updatedog
            var command = new UpdateDogByIdCommand(dogToUpdate, dogId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<Dog>(result);
            //kolla om hunden har det uppdaterade namnet 
            Assert.That(result.Name, Is.EqualTo(dogToUpdate.Name));
        }

    }
}