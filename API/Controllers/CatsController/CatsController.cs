using Microsoft.AspNetCore.Mvc;
using Application.Dtos;
using MediatR;
using Application.Queries.Cats.GetAll;
using Application.Queries.Cats.GetById;
using Application.Commands.Cats.AddCat;
using Application.Commands.Cats.UpdateCat;
using Application.Commands.Cats.DeleteCat;
using Application.Queries.Cats.GetCatsByBreedAndWeight;

namespace API.Controllers.CatsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatsController : ControllerBase
    {
        internal readonly IMediator _mediator;
        private readonly ILogger<CatsController> _logger;
        public CatsController(IMediator mediator, ILogger<CatsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        // Get all Cats from database
        [HttpGet]
        [Route("getAllCats")]
        public async Task<IActionResult> GetAllCats()
        {
            try
            {
                _logger.LogInformation("Försöker hämta alla katter");
                return Ok(await _mediator.Send(new GetAllCatsQuery()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ett fel inträffade vid hämtning av alla katter");
                return StatusCode(500, "Ett internt serverfel inträffade");
            }

        }

        // Get a cat by Id
        [HttpGet]
        [Route("getCatById/{catId}")]
        public async Task<IActionResult> GetCatById(Guid catId)
        {
            //try
            //{
            //    return Ok(await _mediator.Send(new GetCatByIdQuery(catId)));
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}
            try
            {
                _logger.LogInformation($"Attempting to get cat with ID: {catId}");
                var cat = await _mediator.Send(new GetCatByIdQuery(catId));
                if (cat == null)
                {
                    _logger.LogWarning($"Cat with ID {catId} not found.");
                    return NotFound($"Cat with ID {catId} not found.");
                }
                return Ok(cat);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting cat by ID: {catId}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // Create a new cat 
        [HttpPost]
        [Route("addNewCat")]
        public async Task<IActionResult> AddCat([FromBody] CatDto newCat)
        {
            try
            {
                _logger.LogInformation("Försöker lägga till en ny katt");
                return Ok(await _mediator.Send(new AddCatCommand(newCat)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ett fel inträffade vid tillägg av en ny katt");
                return StatusCode(500, "Ett internt serverfel inträffade");
            }

        }

        // Update a specific cat
        [HttpPut]
        [Route("updateCat/{updatedCatId}")]
        public async Task<IActionResult> UpdateCat([FromBody] CatDto updatedCat, Guid updatedCatId)
        {
            try
            {
                _logger.LogInformation($"Försöker uppdatera katt med ID: {updatedCatId}");
                var result = await _mediator.Send(new UpdateCatByIdCommand(updatedCat, updatedCatId));
                if (result == null)
                {
                    _logger.LogWarning($"Uppdatering misslyckades, katt med ID {updatedCatId} hittades inte.");
                    return NotFound($"Katt med ID {updatedCatId} hittades inte.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ett fel inträffade vid uppdatering av katt med ID: {updatedCatId}");
                return StatusCode(500, "Ett internt serverfel inträffade");
            }
            //try
            //{
            //    return Ok(await _mediator.Send(new UpdateCatByIdCommand(updatedCat, updatedCatId)));
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}

        }

        // Deletes specific cat
        [HttpDelete]
        [Route("deletecat/{Id}")]
        public async Task<IActionResult> DeleteCat(Guid Id)
        {
            try
            {
                _logger.LogInformation($"Försöker radera katt med ID: {Id}");
                var result = await _mediator.Send(new DeleteCatByIdCommand(Id));
                if (result == null)
                {
                    _logger.LogWarning($"Radering misslyckades, katt med ID {Id} hittades inte.");
                    return NotFound($"Katt med ID {Id} hittades inte.");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ett fel inträffade vid radering av katt med ID: {Id}");
                return StatusCode(500, "Ett internt serverfel inträffade");
            }
            //var command = new DeleteCatByIdCommand(Id);
            //var result = await _mediator.Send(command);

            //if (result != null)
            //{
            //    return NoContent(); // Om borttagningsoperationen lyckades, returnera information om borttagen katt
            //}

            //return NotFound("Cat Finns inte med i listan"); // Om katten inte hittades, returnera NotFound

        }
        [HttpGet("search")]
        public async Task<IActionResult> GetCatsByBreedAndWeight([FromQuery] string? breed, [FromQuery] int? weight)
        {
            //try
            //{
            //    var query = new GetCatsByBreedAndWeightQuery(breed, weight);
            //    var cats = await _mediator.Send(query);
            //    return Ok(cats);
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}
            try
            {
                _logger.LogInformation("Försöker hämta katter baserat på ras och vikt");
                var query = new GetCatsByBreedAndWeightQuery(breed, weight);
                var cats = await _mediator.Send(query);
                return Ok(cats);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ett fel inträffade vid hämtning av katter baserat på ras och vikt");
                return StatusCode(500, "Ett internt serverfel inträffade");
            }
        }
    }
}
