using FluentValidation;
using OrderService.Commands;

namespace OrderService.Validator;

public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
    {
        RuleFor(co => co.CustomerId).NotEmpty();
        RuleForEach(co => co.OrderItems).ChildRules(item =>
        {
            item.RuleFor(oi => oi.Quantity).NotEmpty();
            item.RuleFor(oi => oi.ProductId).NotEmpty();
        });
    }
}
