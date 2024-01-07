using Application.Dtos;
using Application.Validators.Dog;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class UserAnimalValidator : AbstractValidator<UserAnimalDto>
    {
        public UserAnimalValidator()
        {
            RuleFor(userAnimal => userAnimal.UserName)
                .NotEmpty().WithMessage("User name cannot be empty")
                .Length(2, 50).WithMessage("User name must be between 2 and 50 characters");

            RuleFor(userAnimal => userAnimal.UserId)
                .NotEqual(Guid.Empty).WithMessage("User ID cannot be empty");

            // Validera varje hund i listan Dogs
            RuleForEach(userAnimal => userAnimal.Dogs)
                .SetValidator(new DogValidator());

            // Validera varje katt i listan Cats
            RuleForEach(userAnimal => userAnimal.Cats)
                .SetValidator(new CatValidator());

            // Validera varje fågel i listan Birds
            RuleForEach(userAnimal => userAnimal.Birds)
                .SetValidator(new BirdValidator());
        }
    }
}
