using Microsoft.AspNetCore.Mvc;
using Application.Dtos;
using MediatR;
using Application;
using FluentValidation;
using Application.Validators.Dog;
using Microsoft.AspNetCore.Identity;
using Application.Commands.Users.DeleteUser;
using Application.Commands.Users.UpdateUser;
using Application.Queries.Users.GetById;
using Application.Queries.Users.GetAll;
using Application.Validators;


namespace API.Controllers.Usercontroller
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        internal readonly IMediator _mediator;
        private readonly UserValidator _userValidator;
        private readonly GuidValidator _guidValidator;
        public UsersController(IMediator mediator, UserValidator userValidator, GuidValidator guidValidator)
        {
            _mediator = mediator;
            _userValidator = userValidator;
            _guidValidator = guidValidator;
        }


        // ------------------------------------------------------------------------------------------------------
        // Get all users
        [HttpGet]
        [Route("getAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                return Ok(await _mediator.Send(new GetAllUsersQuery()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        // ------------------------------------------------------------------------------------------------------
        // Get User by Id
        [HttpGet]
        [Route("getUserById")]
        public async Task<IActionResult> GetUserById(Guid UserId)
        {
            var validatedId = _guidValidator.Validate(UserId);
            if (!validatedId.IsValid)
            {
                return BadRequest(validatedId.Errors.ConvertAll(error => error.ErrorMessage));
            }


            var user = await _mediator.Send(new GetUserByIdQuery(UserId));
            return user != null ? Ok(user) : NotFound("User not found.");

            //try
            //{
            //    return Ok(await _mediator.Send(new GetUserByIdQuery(UserId)));
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}
        }
        // ------------------------------------------------------------------------------------------------------
        // Update Specific User
        [HttpPut]
        [Route("updateUser/{updatedUserId}")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto updatedUserDto, Guid updatedUserId, string newPassword)
        {
            var userValidationResult = _userValidator.Validate(updatedUserDto);
            var guidValidationResult = _guidValidator.Validate(updatedUserId);

            if (!userValidationResult.IsValid || !guidValidationResult.IsValid)
            {
                return BadRequest("Ogiltig data för uppdatering.");
            }

            try
            {
                var command = new UpdateUserByIdCommand(updatedUserDto, updatedUserId, newPassword);
                var result = await _mediator.Send(command);

                if (result == null)
                {
                    return NotFound("User not found.");
                }

                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                // Handle not found exception
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                // Handle validation exceptions
                return BadRequest(ex.Errors);
            }
            catch (Exception ex)
            {
                // Handle any other unexpected exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        // ------------------------------------------------------------------------------------------------------
        // Delete user by Id
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteUserById(Guid id)
        {
            var guidValidationResult = _guidValidator.Validate(id);
            if (!guidValidationResult.IsValid)
            {
                return BadRequest("Ogiltigt User ID angivet.");
            }

            var result = await _mediator.Send(new DeleteUserByIdCommand(id));
            return result != null ? NoContent() : NotFound("User not found.");

            //var user = await _mediator.Send(new DeleteUserByIdCommand(id));

            //if (user != null)
            //{
            //    return NoContent();
            //}
            //return NotFound();
        }
        // ------------------------------------------------------------------------------------------------------

    }
}
