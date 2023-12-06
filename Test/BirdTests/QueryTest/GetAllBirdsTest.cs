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
        private RealDatabase _RealDatabase;

        [SetUp]
        public void SetUp()
        {
            // Initialize the handler and mock database before each test
            _RealDatabase = new RealDatabase();
            _handler = new GetAllBirdsQueryHandler(_RealDatabase);

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
            Assert.That(birds.Count, Is.EqualTo(_RealDatabase.Birds.Count));
        }
    }
}
