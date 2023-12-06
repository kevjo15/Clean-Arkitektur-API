using Microsoft.AspNetCore.Mvc;
using Application.Dtos;
using MediatR;
using Application.Queries.Cats.GetAll;
using Application.Queries.Cats.GetById;
using Application.Commands.Cats.AddCat;
using Application.Commands.Cats.UpdateCat;
using Application.Commands.Cats.DeleteCat;

namespace API.Controllers.CatsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatsController : ControllerBase
    {
        internal readonly IMediator _mediator;
        public CatsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Get all Cats from database
        [HttpGet]
        [Route("getAllCats")]
        public async Task<IActionResult> GetAllCats()
        {
            return Ok(await _mediator.Send(new GetAllCatsQuery()));
            //return Ok("GET ALL Cats");
        }

        // Get a cat by Id
        [HttpGet]
        [Route("getCatById/{catId}")]
        public async Task<IActionResult> GetCatById(Guid catId)
        {
            return Ok(await _mediator.Send(new GetCatByIdQuery(catId)));
        }

        // Create a new cat 
        [HttpPost]
        [Route("addNewCat")]
        public async Task<IActionResult> AddCat([FromBody] CatDto newCat)
        {
            return Ok(await _mediator.Send(new AddCatCommand(newCat)));
        }

        // Update a specific cat
        [HttpPut]
        [Route("updateCat/{updatedCatId}")]
        public async Task<IActionResult> UpdateCat([FromBody] CatDto updatedCat, Guid updatedCatId)
        {
            return Ok(await _mediator.Send(new UpdateCatByIdCommand(updatedCat, updatedCatId)));
        }

        // Deletes specific cat
        [HttpDelete]
        [Route("deletecat/{Id}")]
        public async Task<IActionResult> DeleteCat(Guid Id)
        {
            var command = new DeleteCatByIdCommand(Id);
            var result = await _mediator.Send(command);

            if (result != null)
            {
                return NoContent(); // Om borttagningsoperationen lyckades, returnera information om borttagen katt
            }

            return NotFound("Cat Finns inte med i listan"); // Om katten inte hittades, returnera NotFound

        }
    }
}
