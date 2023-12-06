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
            RuleFor(Dog => Dog.Name)
                .NotEmpty().WithMessage("Dog name can not be empty!")
                .NotNull().WithMessage("Dog name can not be NULL");
        }
    }
}
