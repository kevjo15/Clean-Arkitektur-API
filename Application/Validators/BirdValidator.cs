using Application.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class BirdValidator : AbstractValidator<BirdDto>
    {
        public BirdValidator()
        {
            RuleFor(bird => bird.Name)
                .NotEmpty().WithMessage("Bird name cannot be empty")
                .Length(2, 50).WithMessage("Bird name must be between 2 and 50 characters");

            RuleFor(bird => bird.Color)
                .NotEmpty().WithMessage("Bird color cannot be empty");

        }
    }
}
