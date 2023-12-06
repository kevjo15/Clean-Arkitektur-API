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
            var command = new UpdateCatByIdCommand(updatedCat: new CatDto { Name = "UpdatedCatName" }, id: initialCat.Id, likesToPlay: false);

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
        [Test]
        public async Task Handle_Update_Correct_Cat_By_Id()
        {
            // Arrange
            var catId = new Guid("12345678-1234-5678-1234-567812345601");
            var catName = "Börje";
            var dto = new CatDto { Name = catName, LikesToPlay = false };
            var command = new UpdateCatByIdCommand(dto, catId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.Name, Is.EqualTo(catName));
            Assert.IsFalse(result.LikesToPlay);
        }
        [Test]
        public async Task Handle_Update_Correct_Cat_By_Id()
        {
            // Arrange
            var catId = new Guid("12345678-1234-5678-1234-567812345601");
            var catName = "Åke";

            var command = new UpdateCatByIdCommand(new CatDto { Name = catName, LikesToPlay = false }, catId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.AreEqual(catName, result.Name);
            Assert.IsFalse(result.LikesToPlay);
        }

    }
}
