using Application.Queries.Users.GetByUsername.FindUserByUsername;
using Domain.Models;
using Infrastructure.Database.Repositories.Users;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.UserTests.QueryTest
{
    [TestFixture]
    public class FindUserByUsernameTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private FindUserByUsernameQueryHandler _handler;
        private Mock<ILogger<FindUserByUsernameQueryHandler>> _loggerMock;

        [SetUp]
        public void SetUp()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _loggerMock = new Mock<ILogger<FindUserByUsernameQueryHandler>>();
            _handler = new FindUserByUsernameQueryHandler(_userRepositoryMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task Handle_ValidUsername_ReturnsUser()
        {
            // Arrange
            var username = "JohnDoe";
            var user = new User { Id = Guid.NewGuid(), UserName = username };
            _userRepositoryMock.Setup(repo => repo.FindByUsernameAsync(username)).ReturnsAsync(user);

            var query = new FindUserByUsernameQuery(username);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.UserName, Is.EqualTo(username));
        }

        [Test]
        public void Handle_InvalidUsername_ThrowsArgumentException()
        {
            // Arrange
            var invalidUsername = "";

            var query = new FindUserByUsernameQuery(invalidUsername);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(query, CancellationToken.None));
        }

        [Test]
        public void Handle_NonExistingUsername_ThrowsKeyNotFoundException()
        {
            // Arrange
            var nonExistingUsername = "NotExist";
            _userRepositoryMock.Setup(repo => repo.FindByUsernameAsync(nonExistingUsername)).ReturnsAsync((User)null);

            var query = new FindUserByUsernameQuery(nonExistingUsername);

            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(query, CancellationToken.None));
        }
    }
}