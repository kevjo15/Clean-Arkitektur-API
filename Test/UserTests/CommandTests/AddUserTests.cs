using Application.Commands.Users.AddUser;
using Application.Dtos;
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
    public class AddUserTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private AddUserCommandHandler _handler;
        private Mock<ILogger<AddUserCommandHandler>> _loggerMock;

        [SetUp]
        public void SetUp()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _loggerMock = new Mock<ILogger<AddUserCommandHandler>>();
            _handler = new AddUserCommandHandler(_userRepositoryMock.Object, _loggerMock.Object);
        }
        [Test]
        public async Task Handle_ValidRequest_AddsUser()
        {
            // Arrange
            var newUserDto = new UserDto { Username = "TestUser", Password = "Password123" };
            var command = new AddUserCommand(newUserDto);

            var createdUser = new User
            {
                Id = Guid.NewGuid(),
                UserName = newUserDto.Username,
                // Anta att lösenordet hashas korrekt
            };

            _userRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<User>())).ReturnsAsync(createdUser);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(newUserDto.Username, result.UserName);
            // Kontrollera andra relevanta delar av resultaten
        }
        [Test]
        public void Handle_AddThrowsException_ThrowsException()
        {
            // Arrange
            var newUserDto = new UserDto { Username = "TestUser", Password = "Password123" };
            var command = new AddUserCommand(newUserDto);

            _userRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<User>()))
                               .ThrowsAsync(new InvalidOperationException("Test Exception"));

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}