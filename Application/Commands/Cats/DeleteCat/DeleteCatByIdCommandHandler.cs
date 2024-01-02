using Domain.Models;
using MediatR;
using System;
using Infrastructure.Database;
using Infrastructure.Database.Repositories.Cats;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Cats.DeleteCat
{
    public class DeleteCatByIdCommandHandler : IRequestHandler<DeleteCatByIdCommand, Cat>
    {
        private readonly ICatRepository _catRepository;
        private readonly ILogger<DeleteCatByIdCommandHandler> _logger;

        public DeleteCatByIdCommandHandler(ICatRepository catRepository, ILogger<DeleteCatByIdCommandHandler> logger)
        {
            _catRepository = catRepository;
            _logger = logger;
        }

        public async Task<Cat> Handle(DeleteCatByIdCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Attempting to delete cat with ID: {request.Id}");

            var catToDelete = await _catRepository.GetByIdAsync(request.Id);
            if (catToDelete == null)
            {
                _logger.LogWarning($"Cat with ID: {request.Id} was not found.");
                // Du kan överväga att returnera null, eller ett anpassat objekt/resultat som representerar ett misslyckande
                return null;
            }

            try
            {
                await _catRepository.DeleteAsync(request.Id);
                _logger.LogInformation($"Cat with ID: {request.Id} has been successfully deleted.");
                return catToDelete;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting cat with ID: {request.Id}");
                // Hantera felet på lämpligt sätt, t.ex. genom att kasta ett anpassat undantag
                throw;
            }

            //var catToDelete = await _catRepository.GetByIdAsync(request.Id);
            //if (catToDelete == null)
            //{
            //    throw new InvalidOperationException("No cat with the given ID was found.");
            //}

            //await _catRepository.DeleteAsync(request.Id);

            //return catToDelete;
        }
    }
}
