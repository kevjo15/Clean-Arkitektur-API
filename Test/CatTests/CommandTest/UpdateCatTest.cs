using Application.Commands.Cats.UpdateCat;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Database;

namespace Test.CatTests.CommandTest
{
    public class UpdateCatTest
    {
        private UpdateCatByIdCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void Setup()
        {
            _mockDatabase = new MockDatabase();
            _handler = new UpdateCatByIdCommandHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_UpdatesCatInDatabase()
        {
            // Arrange
            var initialCat = new Cat { Id = Guid.NewGuid(), Name = "InitialCatName" };
            _mockDatabase.Cats.Add(initialCat);

            //skapar en instans av updateCat
            var command = new UpdateCatByIdCommand( updatedCat: new CatDto { Name = "UpdatedCatName" }, id: initialCat.Id, likesToPlay: false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<Cat>(result);

            //kolla om hunden har det uppdaterade namnet 
            Assert.That(result.Name, Is.EqualTo("UpdatedCatName"));

            // kolla om hunden har uppdaterats i mocken också
            var updatedCatInDatabase = _mockDatabase.Cats.FirstOrDefault(cat => cat.Id == command.Id);
            Assert.That(updatedCatInDatabase, Is.Not.Null);
            Assert.That(updatedCatInDatabase.Name, Is.EqualTo("UpdatedCatName"));
        }
    }
}
