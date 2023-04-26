using CartService.Commands;
using CartService.Contracts;
using CartService.Services;
using FluentValidation;

namespace CartService.Validation;

public class UpdateCartItemValidator : AbstractValidator<UpdateCartItemCommand>
{
	public UpdateCartItemValidator()
	{
		RuleFor(ci => ci.ShoppingCartItemId).NotEmpty();
		RuleFor(ci => ci.Quantity).NotEmpty();
	}
}
