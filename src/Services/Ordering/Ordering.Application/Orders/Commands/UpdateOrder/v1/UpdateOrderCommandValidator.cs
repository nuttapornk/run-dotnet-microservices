using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Commands.UpdateOrder.v1;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(a => a.Username)
            .NotNull()
            .NotEmpty().WithMessage("{Username} is required.")
            .MaximumLength(50);

        RuleFor(a => a.EmailAddress)
            .NotEmpty().WithMessage("{EmailAddress} is required.");

        RuleFor(a => a.TotalPrice)
            .NotEmpty().WithMessage("{TotalPrice} is required.")
            .GreaterThan(0).WithMessage("{TotalPrice} should be greater than zero");
    }
}
