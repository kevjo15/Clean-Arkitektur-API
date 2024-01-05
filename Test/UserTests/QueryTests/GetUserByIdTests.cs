using Application.Queries.Users.GetById;
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
    public class GetUserByIdTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private GetUserByIdQueryHandler _handler;
        private Mock<ILogger<GetUserByIdQueryHandler>> _loggerMock;

        [SetUp]
        public void SetUp()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _loggerMock = new Mock<ILogger<GetUserByIdQueryHandler>>();
            _handler = new GetUserByIdQueryHandler(_userRepositoryMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task Handle_ValidId_ReturnsCorrectUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, UserName = "JohnDoe" };
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);

            var query = new GetUserByIdQuery(userId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userId, result.Id);
            Assert.AreEqual("JohnDoe", result.UserName);
        }

        [Test]
        public async Task Handle_InvalidId_ReturnsNull()
        {
            // Arrange
            var invalidUserId = Guid.NewGuid();
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(invalidUserId)).ReturnsAsync((User)null);

            var query = new GetUserByIdQuery(invalidUserId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNull(result);
        }
    }
}
