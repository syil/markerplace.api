using CartService.Data;
using CartService.Data.Enums;
using FluentValidation;
using MarketplaceApi.Utilities;
using MediatR;
using OneOf;
using OneOf.Types;

namespace CartService.Commands;

public record UpdateCartItemCommand : IRequest<OneOf<Success, NotFound, ValidationError>>
{
    public Guid ShoppingCartItemId { get; set; }
    public int Quantity { get; init; }
}

public class UpdateCartItemHandler : IRequestHandler<UpdateCartItemCommand, OneOf<Success, NotFound, ValidationError>>
{
    private readonly IValidator<UpdateCartItemCommand> validator;
    private readonly CartDbContext cartDbContext;

    public UpdateCartItemHandler(IValidator<UpdateCartItemCommand> validator, CartDbContext cartDbContext)
    {
        this.validator = validator;
        this.cartDbContext = cartDbContext;
    }

    public async Task<OneOf<Success, NotFound, ValidationError>> Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new ValidationError(validationResult.ToDictionary());
        }

        var item = await cartDbContext.CartItems.FindAsync(new object[] { request.ShoppingCartItemId }, cancellationToken: cancellationToken);

        if (item is null)
        {
            return new NotFound();
        }

        if (item.Status != CartItemStatus.InCart)
        {
            return new ValidationError($"Cannot update [{item.Status}] cart item");
        }

        if (request.Quantity == 0)
        {
            item.Status = CartItemStatus.Deleted;
        }
        else
        {
            item.Quantity = request.Quantity;
        }

        await cartDbContext.SaveChangesAsync(cancellationToken);

        return new Success();
    }
}