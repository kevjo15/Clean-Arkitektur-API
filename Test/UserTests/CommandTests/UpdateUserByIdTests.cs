using Application.Commands.Users.UpdateUser;
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
    public class UpdateUserByIdTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<ILogger<UpdateUserByIdCommandHandler>> _loggerMock;
        private UpdateUserByIdCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _loggerMock = new Mock<ILogger<UpdateUserByIdCommandHandler>>();
            _handler = new UpdateUserByIdCommandHandler(_userRepositoryMock.Object, _loggerMock.Object);
        }
        [Test]
        public async Task Handle_ValidRequest_UpdatesUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var updateUserDto = new UserDto
            {
                Username = "NewUsername",
                Password = "NewPassword"
            };

            var command = new UpdateUserByIdCommand(updateUserDto, userId, "NewPassword");

            var existingUser = new User { Id = userId, UserName = "OldUsername", UserPassword = "OldPassword" };

            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(existingUser);
            _userRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<User>())).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.UserName, Is.EqualTo("NewUsername"));
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public async Task Handle_UserNotFound_ReturnsNull()
        {
            // Arrange
            var nonExistentUserId = Guid.NewGuid();
            var updateUserDto = new UserDto
            {
                Username = "NewUsername",
                Password = "NewPassword"
            };
            var newPassword = "NewPassword";
            var command = new UpdateUserByIdCommand(updateUserDto, nonExistentUserId, newPassword);

            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(nonExistentUserId)).ReturnsAsync((User)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNull(result);
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(nonExistentUserId), Times.Once);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<User>()), Times.Never);
        }


    }
}
