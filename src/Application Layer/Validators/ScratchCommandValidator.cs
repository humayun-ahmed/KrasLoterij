using System;
using FluentValidation;
using NederlandseLoterij.KrasLoterij.Service.Contracts.DTO;

namespace NederlandseLoterij.KrasLoterij.Validators
{
    public class ScratchCommandValidator : AbstractValidator<ScratchCommand>
    {
        public ScratchCommandValidator()
        {
            RuleFor(s => s.Id).GreaterThan(0);
            RuleFor(s => s.UserId).NotNull().NotEqual(Guid.Empty);
        }
    }
}