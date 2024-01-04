using Application.Commands.UserAnimal.AddUserAnimal;
using Application.Commands.UserAnimal.RemoveUserAnimal;
using Application.Commands.UserAnimal.UpdateUserAnimal;
using Application.Dtos;
using Application.Queries.UserAnimal;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace API.Controllers.UserAnimalController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAnimalsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IValidator<Guid> _guidValidator;
        private readonly IValidator<UserAnimalDto> _userAnimalValidator;

        public UserAnimalsController(IMediator mediator, IValidator<Guid> guidValidator, IValidator<UserAnimalDto> userAnimalValidator)
        {
            _mediator = mediator;
            _guidValidator = guidValidator;
            _userAnimalValidator = userAnimalValidator;
        }

        // GET: api/UserAnimals
        [HttpGet]
        [Route("GetAllUsersWithAnimal")]
        public async Task<IActionResult> GetAllUsersWithAnimals()
        {

            try
            {
                var query = new GetAllUsersWithAnimalsQuery();
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Logga felet här
                return StatusCode(500, "Ett internt serverfel inträffade: " + ex.Message);
            }

            //var query = new GetAllUsersWithAnimalsQuery();
            //var result = await _mediator.Send(query);
            //return Ok(result);
        }

        // POST: api/UserAnimals
        [HttpPost]
        [Route("AddUserAnimal")]
        public async Task<IActionResult> AddUserAnimal(AddUserAnimalCommand command)
        {
            try
            {
                var userValidationResult = _guidValidator.Validate(command.UserId);
                var animalValidationResult = _guidValidator.Validate(command.AnimalModelId);

                if (!userValidationResult.IsValid || !animalValidationResult.IsValid)
                {
                    return BadRequest("Invalid User ID or Animal Model ID.");
                }

                var result = await _mediator.Send(command);

                return result != null ? Ok(result) : BadRequest("Failed to add user animal relationship.");
            }
            catch (Exception ex)
            {
                // Logga felet här
                return StatusCode(500, "Ett fel inträffade vid tillägg av djurrelation: " + ex.Message);
            }


            //var result = await _mediator.Send(command);

            //if (result != null)
            //    return Ok(result);  // Antag att result är ett UserAnimalDto-objekt
            //else
            //    return BadRequest("Failed to add user animal relationship.");
        }

        // DELETE: api/UserAnimals/{userId}/{animalModelId}
        [HttpDelete("DeleteRelationShip/{userId}/{animalModelId}")]
        public async Task<IActionResult> RemoveUserAnimal(Guid userId, Guid animalModelId)
        {
            try
            {
                var userValidationResult = _guidValidator.Validate(userId);
                var animalValidationResult = _guidValidator.Validate(animalModelId);

                if (!userValidationResult.IsValid || !animalValidationResult.IsValid)
                {
                    return BadRequest("Invalid User ID or Animal Model ID.");
                }

                var command = new RemoveUserAnimalCommand(userId, animalModelId);
                var result = await _mediator.Send(command);

                return result ? Ok("Relation removed successfully") : BadRequest("Failed to remove relation");
            }
            catch (Exception ex)
            {
                // Logga felet här
                return StatusCode(500, "Ett fel inträffade vid borttagning av djurrelation: " + ex.Message);
            }

            //var command = new RemoveUserAnimalCommand(userId, animalModelId);
            //var result = await _mediator.Send(command);
            //return result ? Ok("Relation removed successfully") : BadRequest("Failed to remove relation");
        }

        [HttpPut("{userId}/{currentAnimalModelId}/{newAnimalModelId}")]
        public async Task<IActionResult> UpdateUserAnimal(Guid userId, Guid currentAnimalModelId, Guid newAnimalModelId)
        {
                       try
            {
                var userValidationResult = _guidValidator.Validate(userId);
                var currentAnimalValidationResult = _guidValidator.Validate(currentAnimalModelId);
                var newAnimalValidationResult = _guidValidator.Validate(newAnimalModelId);

                if (!userValidationResult.IsValid || !currentAnimalValidationResult.IsValid || !newAnimalValidationResult.IsValid)
                {
                    return BadRequest("Invalid data provided for update.");
                }

                var command = new UpdateUserAnimalCommand(userId, currentAnimalModelId, newAnimalModelId);
                var result = await _mediator.Send(command);

                return result ? Ok("Relation updated successfully") : BadRequest("Failed to update relation");
            }
            catch (Exception ex)
            {
                // Logga felet här
                return StatusCode(500, "Ett fel inträffade vid uppdatering av djurrelation: " + ex.Message);
            }


            //var command = new UpdateUserAnimalCommand(userId, currentAnimalModelId, newAnimalModelId);
            //var result = await _mediator.Send(command);
            //return result ? Ok("Relation updated successfully") : BadRequest("Failed to update relation");
        }
    }
}