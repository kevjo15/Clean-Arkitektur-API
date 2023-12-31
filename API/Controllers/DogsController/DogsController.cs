﻿using Application.Commands.Dogs;
using Application.Commands.Dogs.DeleteDog;
using Application.Commands.Dogs.UpdateDog;
using Application.Dtos;
using Application.Queries.Dogs.GetAll;
using Application.Queries.Dogs.GetById;
using Application.Queries.Dogs.GetDogsByBreedAndWeight;
using Application.Validators;
using Application.Validators.Dog;
using Domain.Models;
using Infrastructure.Database;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            try
            {
                var query = new GetAllDogsQuery();
                var result = await _mediator.Send(query);
                return Ok(result);
                //return Ok(await _mediator.Send(new GetAllDogsQuery()));

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        // Get a dog by Id
        [HttpGet]
        [Route("getDogById/{dogId}")]
        public async Task<IActionResult> GetDogById(Guid dogId)
        {
            var validationResult = _guidValidator.Validate(dogId);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.ConvertAll(error => error.ErrorMessage));
            }

            try
            {
                var result = await _mediator.Send(new GetDogByIdQuery(dogId));
                if (result == null)
                {
                    return NotFound($"Hunden med ID {dogId} hittades inte.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Logga felet här
                return StatusCode(500, "Ett internt serverfel inträffade: " + ex.Message);
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
            if (!validatedDog.IsValid)
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
            var dogValidationResult = _dogValidator.Validate(updatedDog);
            var guidValidationResult = _guidValidator.Validate(updatedDogId);

            if (!dogValidationResult.IsValid || !guidValidationResult.IsValid)
            {
                return BadRequest("Ogiltig data för uppdatering.");
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
            var guidValidationResult = _guidValidator.Validate(Id);
            if (!guidValidationResult.IsValid)
            {
                return BadRequest("Ogiltigt Dog ID angivet.");
            }

            var command = new DeleteDogByIdCommand(Id);
            var result = await _mediator.Send(command);

            if (result != null)
            {
                return NoContent(); // Om borttagningsoperationen lyckades, returnera information om borttagen hund
            }

            return NotFound("Dog Finns inte med i listan"); // Om hunden inte hittades, returnera NotFound

        }

        [HttpGet("search")]
        public async Task<IActionResult> GetDogs([FromQuery] string? breed, [FromQuery] int? weight)
        {
            try
            {
                // Skicka parametrar till en enda query
                var query = new GetDogsByBreedAndWeightQuery(breed, weight);
                var dogs = await _mediator.Send(query);
                return Ok(dogs);
            }
            catch (Exception ex)
            {
                // Logga och hantera undantaget på lämpligt sätt
                return StatusCode(500, "Ett internt serverfel inträffade: " + ex.Message);
            }
        }

    }
}
