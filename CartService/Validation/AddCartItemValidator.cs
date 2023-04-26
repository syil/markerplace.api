using CartService.Commands;
using FluentValidation;

namespace CartService.Validation;

public class AddCartItemValidator : AbstractValidator<AddCartItemCommand>
{
	public AddCartItemValidator()
	{
		RuleFor(ci => ci.ProductId).NotEmpty();
		RuleFor(ci => ci.CustomerId).NotEmpty();
		RuleFor(ci => ci.Quantity).NotEmpty();
	}
}
