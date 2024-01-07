using Application.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Dog
{
    public class DogValidator : AbstractValidator<DogDto>
    {
        public DogValidator()
        {
            RuleFor(dog => dog.Name)
                .NotEmpty().WithMessage("Dog name cannot be empty")
                .Length(2, 100).WithMessage("Dog name must be between 2 and 100 characters");

            RuleFor(dog => dog.Breed)
                .NotEmpty().WithMessage("Dog breed cannot be empty")
                .Length(2, 50).WithMessage("Dog breed must be between 2 and 50 characters");

            RuleFor(dog => dog.Weight)
                .GreaterThanOrEqualTo(0).WithMessage("Dog weight cannot be less than 0");
        }
    }
}
