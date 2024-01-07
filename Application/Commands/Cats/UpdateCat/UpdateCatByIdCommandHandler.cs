using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Database.Repositories.Cats;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Commands.Cats.UpdateCat
{

    public class UpdateCatByIdCommandHandler : IRequestHandler<UpdateCatByIdCommand, Cat>
    {
        private readonly ICatRepository _catRepository;

        private readonly ILogger<UpdateCatByIdCommandHandler> _logger;

        public UpdateCatByIdCommandHandler(ICatRepository catRepository, ILogger<UpdateCatByIdCommandHandler> logger)
        {
            _catRepository = catRepository;
            _logger = logger;
        }

        public async Task<Cat> Handle(UpdateCatByIdCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Attempting to update cat with ID: {request.Id}");

            Cat catToUpdate = await _catRepository.GetByIdAsync(request.Id);
            if (catToUpdate == null)
            {
                _logger.LogWarning($"Cat with ID: {request.Id} was not found.");
                return null; // Eller hantera det på annat sätt beroende på ditt API:s design
            }

            try
            {
                catToUpdate.Name = request.UpdatedCat.Name;
                catToUpdate.LikesToPlay = request.UpdatedCat.LikesToPlay;
                await _catRepository.UpdateAsync(catToUpdate);

                _logger.LogInformation($"Cat with ID: {request.Id} has been successfully updated.");
                return catToUpdate;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating cat with ID: {request.Id}");
                // Hantera felet på lämpligt sätt, t.ex. genom att kasta ett anpassat undantag
                throw;
            }

            //Cat catToUpdate = await _catRepository.GetByIdAsync(request.Id);
            //if (catToUpdate == null)
            //{
            //    return null!;
            //}

            //catToUpdate.Name = request.UpdatedCat.Name;
            //catToUpdate.LikesToPlay = request.UpdatedCat.LikesToPlay;
            //await _catRepository.UpdateAsync(catToUpdate);

            //return catToUpdate;

        }
    }

}