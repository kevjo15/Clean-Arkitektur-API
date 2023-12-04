using Application.Commands.Dogs;
using Application.Commands.Dogs.DeleteDog;
using Application.Commands.Dogs.UpdateDog;
using Application.Dtos;
using Application.Queries.Dogs.GetAll;
using Application.Queries.Dogs.GetById;
using Application.Validators.Dog;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers.DogsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        internal readonly IMediator _mediator;
        private readonly DogValidator _dogValidator;
        private readonly GuidValidator _guidValidator;

        public DogsController(IMediator mediator, DogValidator dogValidator, GuidValidator guidValidator)
        {
            _mediator = mediator;
            _dogValidator = dogValidator;
            _guidValidator = guidValidator;
        }

        // Get all dogs from database
        [HttpGet]
        [Route("getAllDogs")]
        public async Task<IActionResult> GetAllDogs()
        {
            return Ok(await _mediator.Send(new GetAllDogsQuery()));
            //return Ok("GET ALL DOGS");
        }

        // Get a dog by Id
        [HttpGet]
        [Route("getDogById/{dogId}")]
        public async Task<IActionResult> GetDogById(Guid dogId)
        {
            var validatedDog = _guidValidator.Validate(dogId);
            //Error handling
            if (!validatedDog.IsValid)
            {
                return BadRequest(validatedDog.Errors.ConvertAll(errors => errors.ErrorMessage));
            }
            try
            {
                return Ok(await _mediator.Send(new GetDogByIdQuery(dogId)));
            }
            catch (Exception ex) 
            { 
                throw new Exception(ex.Message);
            }
        }

        // Create a new dog 
        [HttpPost]
        [Route("addNewDog")]
        public async Task<IActionResult> AddDog([FromBody] DogDto newDog)
        {
            //Validate Dog
            var validatedDog = _dogValidator.Validate(newDog);

            //Error handling
            if(!validatedDog.IsValid) 
            {
                return BadRequest(validatedDog.Errors.ConvertAll(errors => errors.ErrorMessage));

            }

            try
            {
                return Ok(await _mediator.Send(new AddDogCommand(newDog)));

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Update a specific dog
        [HttpPut]
        [Route("updateDog/{updatedDogId}")]
        public async Task<IActionResult> UpdateDog([FromBody] DogDto updatedDog, Guid updatedDogId)
        {
            var validatedDog = _dogValidator.Validate(updatedDog);
            var guidValidator = _guidValidator.Validate(updatedDogId);

            if (!validatedDog.IsValid)
            {
                return BadRequest(validatedDog.Errors.ConvertAll(errors => errors.ErrorMessage));
            }
            if (!guidValidator.IsValid)
            {
                return BadRequest(guidValidator.Errors.ConvertAll(errors => errors.ErrorMessage));
            }
            try
            {
                return Ok(await _mediator.Send(new UpdateDogByIdCommand(updatedDog, updatedDogId)));

            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        // IMPLEMENT DELETE !!!
        [HttpDelete]
        [Route("deletedog/{Id}")]
        public async Task<IActionResult> DeleteDog(Guid Id)
        {
            var command = new DeleteDogByIdCommand(Id);
            var result = await _mediator.Send(command);

            if (result != null)
            {
                return NoContent(); // Om borttagningsoperationen lyckades, returnera information om borttagen hund
            }

            return NotFound("Dog Finns inte med i listan"); // Om hunden inte hittades, returnera NotFound

        }
    }
}
