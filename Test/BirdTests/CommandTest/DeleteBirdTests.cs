using API.Controllers.BirdsController;
using API.Controllers.CatsController;
using Application.Commands.Birds.DeleteBird;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Database.Repositories.Birds;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.BirdTests.CommandTest
{
    [TestFixture]
    public class DeleteBirdByIdTests
    {
        private Mock<IBirdRepository> _birdRepositoryMock;
        private DeleteBirdByIdCommandHandler _handler;
        private Mock<ILogger<DeleteBirdByIdCommandHandler>> _loggerMock;

        [SetUp]
        public void SetUp()
        {
            _birdRepositoryMock = new Mock<IBirdRepository>();
            _loggerMock = new Mock<ILogger<DeleteBirdByIdCommandHandler>>();
            _handler = new DeleteBirdByIdCommandHandler(_birdRepositoryMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task Handle_ValidId_DeletesBird()
        {
            // Arrange
            var birdId = Guid.NewGuid();
            var bird = new Bird { Id = birdId, Name = "TestBird" };
            _birdRepositoryMock.Setup(repo => repo.GetByIdAsync(birdId)).ReturnsAsync(bird);
            _birdRepositoryMock.Setup(repo => repo.DeleteAsync(birdId)).Returns(Task.CompletedTask);

            var command = new DeleteBirdByIdCommand(birdId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(birdId, result.Id);
            // Kontrollera andra relevanta delar av resultaten
        }
        [Test]
        public void Handle_DeleteThrowsException_ThrowsException()
        {
            // Arrange
            var birdId = Guid.NewGuid();
            _birdRepositoryMock.Setup(repo => repo.GetByIdAsync(birdId)).ReturnsAsync(new Bird { Id = birdId });
            _birdRepositoryMock.Setup(repo => repo.DeleteAsync(birdId)).ThrowsAsync(new InvalidOperationException("Test Exception"));

            var command = new DeleteBirdByIdCommand(birdId);

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
