using Application.Commands.Birds.AddBird;
using Application.Dtos;
using Infrastructure.Database;

namespace Test.BirdTests.CommandTest
{
    [TestFixture]
    public class AddBirdTest
    {
        private MockDatabase _mockDatabase;
        private AddBirdCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockDatabase = new MockDatabase();
            _handler = new AddBirdCommandHandler(_mockDatabase);
        }

        [Test]
        public async Task AddBird_ShouldReturnCorrectBird()
        {
            // Arrange
            var command = new AddBirdCommand(new BirdDto { Name = "New Bird" });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            //var newbirdinDB = _mockDatabase.Birds.FirstOrDefault(bird => bird.Name == "New Bird");

            Assert.IsNotNull(result);
            Assert.That(result.Name, Is.EqualTo("New Bird"));
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
