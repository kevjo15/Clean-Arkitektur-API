using API.Controllers.DogsController;
using Application.Dtos;
using Application.Queries.Dogs;
using Application.Queries.Dogs.GetAll;
using Domain.Models;
using FluentAssertions;
using Infrastructure.Database;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Test.DogTests.QueryTest
{
    [TestFixture]
    public class GetAllDogsTest
    {
        private Mock<IDogRepository> _dogRepositoryMock;
        private GetAllDogsQueryHandler _handler;
        private Mock<ILogger<GetAllDogsQueryHandler>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            _dogRepositoryMock = new Mock<IDogRepository>();
            _loggerMock = new Mock<ILogger<GetAllDogsQueryHandler>>();
            _handler = new GetAllDogsQueryHandler(_dogRepositoryMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task GetAllDogs_WhenDogsExist_ReturnsAllDogs()
        {
            // Arrange
            var fakeDogs = new List<Dog> { new Dog(), new Dog() }; // Använd Dog istället för DogDto
            _dogRepositoryMock.Setup(repo => repo.GetAllDogsAsync())
                              .ReturnsAsync(fakeDogs); // Returnerar Task<List<Dog>>

            // Act
            var query = new GetAllDogsQuery();
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result.Count, Is.EqualTo(2)); // Kontrollerar antal hundar
            _dogRepositoryMock.Verify(repo => repo.GetAllDogsAsync(), Times.Once); // Kontrollerar att metoden kallades en gång
        }
        [Test]
        public async Task GetAllDogs_WhenRepositoryThrowsException_ReturnsEmptyList()
        {
            // Arrange
            _dogRepositoryMock.Setup(repo => repo.GetAllDogsAsync())
                              .ThrowsAsync(new Exception("Database error"));

            // Act
            var query = new GetAllDogsQuery();
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
