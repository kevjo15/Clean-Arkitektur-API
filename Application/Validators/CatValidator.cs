using Application.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class CatValidator : AbstractValidator<CatDto>
    {
        public CatValidator()
        {
            RuleFor(cat => cat.Name)
                .NotEmpty().WithMessage("Cat name cannot be empty")
                .Length(2, 50).WithMessage("Cat name must be between 2 and 50 characters");

            RuleFor(cat => cat.Weight)
                .GreaterThanOrEqualTo(0).WithMessage("Cat age cannot be less than 0");

            RuleFor(cat => cat.Breed)
                .NotEmpty().WithMessage("Cat breed cannot be empty");
        }
    }
}