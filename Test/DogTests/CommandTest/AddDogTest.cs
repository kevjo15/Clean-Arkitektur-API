using Application.Commands.Dogs;
using Application.Dtos;
using Infrastructure.Database;

namespace Test.DogTests.CommandTest
{
    [TestFixture]
    public class AddDogTests
    {
        //private RealDatabase _RealDatabase;
        //private AddDogCommandHandler _handler;

        //[SetUp]
        //public void SetUp()
        //{
        //    // Initialisera mockdatabasen och hanteraren innan varje test
        //    _RealDatabase = new RealDatabase();
        //    _handler = new AddDogCommandHandler(_RealDatabase);
        //}

        //[Test]
        //public async Task Handle_ValidAddDog_ReturnsTrue()
        //{
        //    // Arrange
        //    var command = new AddDogCommand(new DogDto { Name = "New Dog" });

        //    // Act
        //    var result = await _handler.Handle(command, CancellationToken.None);

        //    // Assert
        //    var newdoginDB = _RealDatabase.Dogs.FirstOrDefault(dog => dog.Name == "New Dog");

        //    Assert.IsNotNull(newdoginDB);
        //    Assert.That(newdoginDB.Name, Is.EqualTo("New Dog"));
        //}

        //[Test]
        //public async Task Handle_invalidAddDog_ReturnsTrue()
        //{
        //    // Arrange
        //    var command = new AddDogCommand(new DogDto { Name = "" });

        //    // Act
        //    var result = await _handler.Handle(command, CancellationToken.None);

        //    // Assert
        //    Assert.IsNotNull(result);

        //}

    }
}