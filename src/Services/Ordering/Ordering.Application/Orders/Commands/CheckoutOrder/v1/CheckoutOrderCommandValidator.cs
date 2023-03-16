using FluentValidation;

namespace Ordering.Application.Orders.Commands.CheckoutOrder.v1;

public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
{
    public CheckoutOrderCommandValidator()
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
