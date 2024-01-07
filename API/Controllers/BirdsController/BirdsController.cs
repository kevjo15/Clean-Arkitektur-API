using Application.Commands.Birds.AddBird;
using Application.Commands.Birds.UpdateBird;
using Application.Commands.Birds.DeleteBird;
using Application.Dtos;
using Application.Queries.Birds.GetAll;
using Application.Queries.Birds.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Queries.Birds.GetByColor;
using Application.Validators;

namespace API.Controllers.BirdsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdsController : ControllerBase
    {
        internal readonly IMediator _mediator;
        private readonly BirdValidator _birdValidator;
        private readonly GuidValidator _guidValidator;

        public BirdsController(IMediator mediator, BirdValidator birdValidator, GuidValidator guidValidator)
        {
            _mediator = mediator;
            _birdValidator = birdValidator;
            _guidValidator = guidValidator;
        }

        // Get all Birds from database
        [HttpGet]
        [Route("getAllBirds")]
        public async Task<IActionResult> GetAllBirds()
        {
            try
            {
                return Ok(await _mediator.Send(new GetAllBirdsQuery()));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex}, Ett internt serverfel inträffade");
            }

        }

        // Get a Bird by Id
        [HttpGet]
        [Route("getBirdById/{birdId}")]
        public async Task<IActionResult> GetBirdById(Guid birdId)
        {
            var validationResult = _guidValidator.Validate(birdId);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var bird = await _mediator.Send(new GetBirdByIdQuery(birdId));
            return bird != null ? Ok(bird) : NotFound($"Fågeln med ID {birdId} hittades inte.");

            //return Ok(await _mediator.Send(new GetBirdByIdQuery(birdId)));
        }

        // Create a new Bird 
        [HttpPost]
        [Route("addNewBird")]
        public async Task<IActionResult> AddBird([FromBody] BirdDto newBird)
        {
            var validationResult = _birdValidator.Validate(newBird);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            return Ok(await _mediator.Send(new AddBirdCommand(newBird)));
        }
        // Update a specific Bird
        [HttpPut]
        [Route("updateBird/{updatedBirdId}")]
        public async Task<IActionResult> UpdateBird([FromBody] BirdDto updatedBird, Guid updatedBirdId)
        {
            var birdValidationResult = _birdValidator.Validate(updatedBird);
            var guidValidationResult = _guidValidator.Validate(updatedBirdId);

            if (!birdValidationResult.IsValid || !guidValidationResult.IsValid)
            {
                return BadRequest("Ogiltig data för uppdatering.");
            }
            return Ok(await _mediator.Send(new UpdateBirdByIdCommand(updatedBird, updatedBirdId)));
        }

        // Deletes specific Bird
        [HttpDelete]
        [Route("deletebird/{Id}")]
        public async Task<IActionResult> DeleteBird(Guid Id)
        {

            var guidValidationResult = _guidValidator.Validate(Id);
            if (!guidValidationResult.IsValid)
            {
                return BadRequest("Ogiltigt Bird ID angivet.");
            }

            var result = await _mediator.Send(new DeleteBirdByIdCommand(Id));
            return result != null ? NoContent() : NotFound("Fågeln med ID hittades inte.");

            //var command = new DeleteBirdByIdCommand(Id);
            //var result = await _mediator.Send(command);

            //if (result != null)
            //{
            //    return NoContent(); // Om borttagningsoperationen lyckades, returnera information om borttagen fågel
            //}

            //return NotFound("Cat Finns inte med i listan"); // Om fågeln inte hittades, returnera NotFound

        }
        //GetByAttribute
        [HttpGet("color/{color}")]
        public async Task<IActionResult> GetBirdByAttribute(string color)
        {
            var query = new GetBirdByColorQuery(color);
            var bird = await _mediator.Send(query);
            return bird != null ? Ok(bird) : NotFound();
        }
    }

}
