using Application.Queries.Birds.GetAll;
using Application.Queries.Cats.GetAll;
using Domain.Models;
using Infrastructure.Database;

namespace Test.BirdTests.QueryTest
{
    [TestFixture]
    public class GetAllBirdsTest
    {
        private GetAllBirdsQueryHandler _handler;
        private MockDatabase _mockDatabase;
        private MockDatabase? _temporaryDatabase;

        [SetUp]
        public void SetUp()
        {
            // Initialize the handler and mock database before each test
            _mockDatabase = new MockDatabase();
            _handler = new GetAllBirdsQueryHandler(_mockDatabase);

        }
        [Test]
        public async Task GetAllBirds_ShouldReturnListOfBird()
        {
            // Arrange

            // Act
            var result = await _handler.Handle(new GetAllBirdsQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<List<Bird>>(result);

            var birds = (List<Bird>)result;
            Assert.That(birds.Count, Is.EqualTo(_mockDatabase.Birds.Count));
        }
    }
}
