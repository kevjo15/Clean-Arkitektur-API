//using Application.Commands.Birds.UpdateBird;
//using Application.Dtos;
//using Domain.Models;
//using Infrastructure.Database;

//namespace Test.BirdTests.CommandTest
//{
//    public class UpdateBirdTest
//    {
//        private UpdateBirdByIdCommandHandler _handler;
//        private FakeDatabase _realDatabase;

//        [SetUp]
//        public void Setup()
//        {
//            _realDatabase = new FakeDatabase();
//            _handler = new UpdateBirdByIdCommandHandler(_realDatabase);
//        }

//        [Test]
//        public async Task Handle_UpdatesBirdInDatabase()
//        {
//            // Arrange
//            var birdId = new Guid("12345678-1234-5678-1234-567812345614");
//            var birdToUpdate = new BirdDto { Name = "UpdatedBirdName", CanFly = false };

//            //skapar en instans av updateBird
//            var command = new UpdateBirdByIdCommand(birdToUpdate, birdId);

//            // Act
//            var result = await _handler.Handle(command, CancellationToken.None);

//            // Assert
//            Assert.NotNull(result);
//            Assert.IsInstanceOf<Bird>(result);
//            Assert.That(result.Name, Is.EqualTo(birdToUpdate.Name));
//            Assert.That(result.CanFly = false, Is.False);

//        }
//    }
//}
