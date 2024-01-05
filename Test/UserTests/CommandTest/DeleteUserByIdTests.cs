using Application.Commands.Users.DeleteUser;
using Domain.Models;
using Infrastructure.Database.Repositories.Users;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.UserTests.CommandTest
{
    [TestFixture]
    public class DeleteUserByIdTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private DeleteUserByIdCommandHandler _handler;
        private Mock<ILogger<DeleteUserByIdCommandHandler>> _loggerMock;

        [SetUp]
        public void SetUp()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _loggerMock = new Mock<ILogger<DeleteUserByIdCommandHandler>>();
            _handler = new DeleteUserByIdCommandHandler(_userRepositoryMock.Object, _loggerMock.Object);
        }
        [Test]
        public async Task Handle_ValidId_DeletesUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, UserName = "TestUser" };
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);
            _userRepositoryMock.Setup(repo => repo.DeleteAsync(userId)).Returns(Task.CompletedTask);

            var command = new DeleteUserByIdCommand(userId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userId, result.Id);
            // Kontrollera andra relevanta delar av resultaten
        }
        [Test]
        public void Handle_DeleteThrowsException_ThrowsException()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(new User { Id = userId });
            _userRepositoryMock.Setup(repo => repo.DeleteAsync(userId)).ThrowsAsync(new InvalidOperationException("Test Exception"));

            var command = new DeleteUserByIdCommand(userId);

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
        }

    }
}
