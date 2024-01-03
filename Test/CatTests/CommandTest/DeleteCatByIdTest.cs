//using Application.Commands.Cats.DeleteCat;
//using Infrastructure.Database;


//namespace Test.CatTests.CommandTest
//{
//    [TestFixture]
//    public class DeleteCatByIdTest
//    {
//        private DeleteCatByIdCommandHandler _handler;
//        private FakeDatabase _RealDatabase;

//        [SetUp]
//        public void Setup()
//        {
//            _RealDatabase = new FakeDatabase();
//            _handler = new DeleteCatByIdCommandHandler(_RealDatabase);
//        }

//        [Test]
//        public async Task DeleteCatById_ShouldRemoveCatIfExistingCatIsDeleted()
//        {
//            // Arrange
//            var existingCatId = new Guid("12345678-1234-5678-1234-567812345675");
//            var deleteCommand = new DeleteCatByIdCommand(existingCatId);

//            // Act
//            var result = await _handler.Handle(deleteCommand, new CancellationToken());

//            // Assert
//            var catExistsAfterDeletion = _RealDatabase.Cats.Any(c => c.Id == existingCatId);

//            Assert.IsFalse(catExistsAfterDeletion, "Cat should be deleted from the database");


//        }

//    }

//}
