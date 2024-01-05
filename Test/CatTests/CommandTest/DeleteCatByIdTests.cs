using Application.Commands.Cats.DeleteCat;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Database.Repositories.Cats;
using Microsoft.Extensions.Logging;
using Moq;


namespace Test.CatTests.CommandTest
{
    [TestFixture]
    public class DeleteCatByIdTests
    {
        private Mock<ICatRepository> _catRepositoryMock;
        private DeleteCatByIdCommandHandler _handler;
        private Mock<ILogger<DeleteCatByIdCommandHandler>> _loggerMock;

        [SetUp]
        public void SetUp()
        {
            _catRepositoryMock = new Mock<ICatRepository>();
            _loggerMock = new Mock<ILogger<DeleteCatByIdCommandHandler>>();
            _handler = new DeleteCatByIdCommandHandler(_catRepositoryMock.Object, _loggerMock.Object);
        }
        [Test]
        public async Task Handle_ValidId_DeletesCat()
        {
            // Arrange
            var catId = Guid.NewGuid();
            var cat = new Cat { Id = catId, Name = "TestCat" };
            _catRepositoryMock.Setup(repo => repo.GetByIdAsync(catId)).ReturnsAsync(cat);
            _catRepositoryMock.Setup(repo => repo.DeleteAsync(catId)).Returns(Task.CompletedTask);

            var command = new DeleteCatByIdCommand(catId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(catId, result.Id);
            // Kontrollera andra relevanta delar av resultaten
        }
        [Test]
        public void Handle_DeleteThrowsException_ThrowsException()
        {
            // Arrange
            var catId = Guid.NewGuid();
            _catRepositoryMock.Setup(repo => repo.GetByIdAsync(catId)).ReturnsAsync(new Cat { Id = catId });
            _catRepositoryMock.Setup(repo => repo.DeleteAsync(catId)).ThrowsAsync(new InvalidOperationException("Test Exception"));

            var command = new DeleteCatByIdCommand(catId);

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }

}
