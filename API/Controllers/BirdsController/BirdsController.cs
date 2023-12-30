using Application.Commands.Birds.AddBird;
using Application.Commands.Birds.UpdateBird;
using Application.Commands.Birds.DeleteBird;
using Application.Dtos;
using Application.Queries.Birds.GetAll;
using Application.Queries.Birds.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Queries.Birds.GetByColor;

namespace API.Controllers.BirdsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdsController : ControllerBase
    {
        internal readonly IMediator _mediator;

        public BirdsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Get all Birds from database
        [HttpGet]
        [Route("getAllBirds")]
        public async Task<IActionResult> GetAllBirds()
        {
            return Ok(await _mediator.Send(new GetAllBirdsQuery()));
        }

        // Get a Bird by Id
        [HttpGet]
        [Route("getBirdById/{birdId}")]
        public async Task<IActionResult> GetBirdById(Guid birdId)
        {
            return Ok(await _mediator.Send(new GetBirdByIdQuery(birdId)));
        }

        // Create a new Bird 
        [HttpPost]
        [Route("addNewBird")]
        public async Task<IActionResult> AddBird([FromBody] BirdDto newBird)
        {
            return Ok(await _mediator.Send(new AddBirdCommand(newBird)));
        }
        // Update a specific Bird
        [HttpPut]
        [Route("updateBird/{updatedBirdId}")]
        public async Task<IActionResult> UpdateBird([FromBody] BirdDto updatedBird, Guid updatedBirdId)
        {
            return Ok(await _mediator.Send(new UpdateBirdByIdCommand(updatedBird, updatedBirdId)));
        }

        // Deletes specific Bird
        [HttpDelete]
        [Route("deletebird/{Id}")]
        public async Task<IActionResult> DeleteBird(Guid Id)
        {
            var command = new DeleteBirdByIdCommand(Id);
            var result = await _mediator.Send(command);

            if (result != null)
            {
                return NoContent(); // Om borttagningsoperationen lyckades, returnera information om borttagen fågel
            }

            return NotFound("Cat Finns inte med i listan"); // Om fågeln inte hittades, returnera NotFound

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
