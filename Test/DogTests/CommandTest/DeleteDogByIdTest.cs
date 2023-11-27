using API.Controllers.DogsController;
using Application.Commands.Dogs.DeleteDog;
using Application.Queries.Dogs.GetById;
using Domain.Models;
using Infrastructure.Database;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.DogTests.CommandTest
{
    [TestFixture]
    public class DeleteDogByIdTest
    {

        private MockDatabase _mockDatabase;
        private DogsController _controller;
        private Mock<IMediator> _mediatorMock;

        [SetUp]
        public void SetUp()
        {
            // Skapa en Moq Mock för IMediator
            _mediatorMock = new Mock<IMediator>();

            // Konfigurera IMediator att returnera null när DeleteDogByIdCommand skickas
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteDogByIdCommand>(), default(CancellationToken)))
             .Returns(Task.FromResult((Dog)null));


            // Initialize the handler and mock database before each test
            _mockDatabase = new MockDatabase();
            _controller = new DogsController(_mediatorMock.Object);
        }
        [Test]
        public async Task DeleteDogById_ShouldReturnNoContentWhenDogIsDeleted()
        {
            // Arrange
            var dogId = _mockDatabase.Dogs[2].Id;
            //var dogId = new Guid("12345678-1234-5678-1234-567812345678");

            // Act
            var result = await _controller.DeleteDog(dogId);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
            Console.WriteLine($"Test result: {result}");
        }
        [Test]
        public async Task DeleteDog_ShouldReturnNotFoundWhenDogIsNotFound()
        {
            // Arrange
            var nonExistentDogId = Guid.NewGuid();

            // Act
            var result = await _controller.DeleteDog(nonExistentDogId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}
