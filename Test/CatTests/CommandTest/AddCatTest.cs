using Application.Commands.Cats.AddCat;
using Application.Dtos;
using Infrastructure.Database;

namespace Test.CatTests.CommandTest
{
    [TestFixture]
    public class AddCatTest
    {
        private RealDatabase _RealDatabase;
        private AddCatCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _RealDatabase = new RealDatabase();
            _handler = new AddCatCommandHandler(_RealDatabase);
        }
        [Test]
        public async Task AddCat_ShouldReturnCorrectCat()
        {
            // Arrange
            var command = new AddCatCommand(new CatDto { Name = "New Cat" });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            var newcatinDB = _RealDatabase.Cats.FirstOrDefault(cat => cat.Name == "New Cat");

            Assert.IsNotNull(newcatinDB);
            Assert.That(newcatinDB.Name, Is.EqualTo("New Cat"));
        }
        [Test]
        public async Task Handle_invalidAddCat_ReturnsTrue()
        {
            // Arrange
            var command = new AddCatCommand(new CatDto { Name = "" });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);

        }
    }
}
