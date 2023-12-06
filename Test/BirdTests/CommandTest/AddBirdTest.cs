using Application.Commands.Birds.AddBird;
using Application.Dtos;
using Infrastructure.Database;

namespace Test.BirdTests.CommandTest
{
    [TestFixture]
    public class AddBirdTest
    {
        private RealDatabase _realDatabase;
        private AddBirdCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _realDatabase = new RealDatabase();
            _handler = new AddBirdCommandHandler(_realDatabase);
        }

        [Test]
        public async Task AddBird_ShouldReturnCorrectBird()
        {
            // Arrange
            var command = new AddBirdCommand(new BirdDto { Name = "New Bird" });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            var newbirdinDB = _realDatabase.Birds.FirstOrDefault(bird => bird.Name == "New Bird");

            Assert.IsNotNull(newbirdinDB);
            Assert.That(newbirdinDB.Name, Is.EqualTo("New Bird"));
        }

        [Test]
        public async Task Handle_invalidAddBird_ReturnsTrue()
        {
            // Arrange
            var command = new AddBirdCommand(new BirdDto { Name = "" });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);

        }
    }
}
