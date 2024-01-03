using Application.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class GuidValidator : AbstractValidator<Guid>
    {
        public GuidValidator()
        {
            RuleFor(guid => guid).NotNull().WithMessage("Guid cant be NULL")
                .NotEmpty().WithMessage("Guid cant be empty")
                .NotEqual(Guid.Empty).WithMessage("Guid should not be empty");
        }
    }
}
