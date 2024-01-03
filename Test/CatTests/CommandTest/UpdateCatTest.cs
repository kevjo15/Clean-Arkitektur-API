//using Application.Commands.Cats.UpdateCat;
//using Application.Dtos;
//using Domain.Models;
//using Infrastructure.Database;

//namespace Test.CatTests.CommandTest
//{
//    public class UpdateCatTest
//    {
//        private UpdateCatByIdCommandHandler _handler;
//        private FakeDatabase _RealDatabase;

//        [SetUp]
//        public void Setup()
//        {
//            _RealDatabase = new FakeDatabase();
//            _handler = new UpdateCatByIdCommandHandler(_RealDatabase);
//        }

//        [Test]
//        public async Task Handle_UpdatesCatInDatabase()
//        {
//            // Arrange
//            var catId = new Guid("12345678-1234-5678-1234-567812345613");
//            var catToUpdate = new CatDto { Name = "UpdatedCatName", LikesToPlay = true };

//            //skapar en instans av updateCat
//            var command = new UpdateCatByIdCommand(catToUpdate, catId);

//            // Act
//            var result = await _handler.Handle(command, CancellationToken.None);

//            // Assert
//            Assert.NotNull(result);
//            Assert.IsInstanceOf<Cat>(result);
//            //kolla om katten har det uppdaterade namnet 
//            Assert.That(result.Name, Is.EqualTo(catToUpdate.Name));
//        }
//    }
//}
