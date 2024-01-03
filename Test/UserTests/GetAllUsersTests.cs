using Application.Queries.Users.GetAll;
using Domain.Models;
using Infrastructure.Database.Repositories.Users;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.UserTests
{
    [TestFixture]
    public class GetAllUsersTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<ILogger<GetAllUsersQueryHandler>> _loggerMock;
        private GetAllUsersQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _loggerMock = new Mock<ILogger<GetAllUsersQueryHandler>>();
            _handler = new GetAllUsersQueryHandler(_userRepositoryMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task GetAllUsers_WhenUsersExist_ReturnsAllUsers()
        {
            // Arrange
            var fakeUsers = new List<User> { new User(), new User() };
            _userRepositoryMock.Setup(repo => repo.GetAllUsersAsync())
                              .ReturnsAsync(fakeUsers);

            // Act
            var query = new GetAllUsersQuery();
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            _userRepositoryMock.Verify(repo => repo.GetAllUsersAsync(), Times.Once);
        }
        [Test]
        public async Task GetAllUsers_WhenRepositoryThrowsException_ReturnsEmptyList()
        {
            // Arrange
            _userRepositoryMock.Setup(repo => repo.GetAllUsersAsync())
                              .ThrowsAsync(new Exception("Database error"));

            // Act
            var query = new GetAllUsersQuery();
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
            _loggerMock.Verify(logger => logger.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
                Times.Once);
        }

    }
}
