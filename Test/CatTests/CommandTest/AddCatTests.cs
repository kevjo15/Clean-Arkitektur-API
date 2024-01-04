using Application.Commands.Cats.AddCat;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Database.Repositories.Cats;
using Microsoft.Extensions.Logging;
using Moq;

namespace Test.CatTests.CommandTest
{
    [TestFixture]
    public class AddCatTests
    {
        private Mock<ICatRepository> _catRepositoryMock;
        private AddCatCommandHandler _handler;
        private Mock<ILogger<AddCatCommandHandler>> _loggerMock;

        [SetUp]
        public void SetUp()
        {
            _catRepositoryMock = new Mock<ICatRepository>();
            _loggerMock = new Mock<ILogger<AddCatCommandHandler>>();
            _handler = new AddCatCommandHandler(_catRepositoryMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task Handle_ValidRequest_AddsCat()
        {
            // Arrange
            var newCatDto = new CatDto { Name = "Whiskers", Breed = "Tabby", Weight = 10 };
            var command = new AddCatCommand(newCatDto);

            // Skapa ett Cat-objekt som du förväntar dig att AddAsync ska returnera
            var createdCat = new Cat
            {
                Id = Guid.NewGuid(), // Anta att ett nytt ID genereras av AddAsync
                Name = newCatDto.Name,
                Breed = newCatDto.Breed,
                Weight = newCatDto.Weight
            };

            // Setup mock så att den returnerar det skapade Cat-objektet när AddAsync anropas
            _catRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Cat>())).ReturnsAsync(createdCat);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            //Assert.AreEqual(createdCat.Id, result.Id);
            Assert.AreEqual(newCatDto.Name, result.Name);
            Assert.AreEqual(newCatDto.Breed, result.Breed);
            Assert.AreEqual(newCatDto.Weight, result.Weight);
        }

        [Test]
        public void Handle_AddThrowsException_ThrowsException()
        {
            // Arrange
            var newCatDto = new CatDto { Name = "Whiskers", Breed = "Tabby", Weight = 10 };
            var command = new AddCatCommand(newCatDto);

            _catRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Cat>())).ThrowsAsync(new InvalidOperationException());

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
