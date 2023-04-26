using CartService.Contracts;
using CartService.Data;
using CartService.Data.Enums;
using CartService.Services;
using FluentValidation;
using Mapster;
using MarketplaceApi.Utilities;
using MediatR;
using OneOf;
using OneOf.Types;

namespace CartService.Commands;

public record AddCartItemCommand(Guid ProductId, Guid CustomerId, int Quantity) : IRequest<OneOf<Success<CartProductItem>, ValidationError>>;

public class AddCartItemHandler : IRequestHandler<AddCartItemCommand, OneOf<Success<CartProductItem>, ValidationError>>
{
    private readonly IValidator<AddCartItemCommand> validator;
    private readonly CartDbContext cartDbContext;
    private readonly ICatalogService catalogService;

    public AddCartItemHandler(IValidator<AddCartItemCommand> validator, CartDbContext cartDbContext, ICatalogService catalogService)
    {
        this.validator = validator;
        this.cartDbContext = cartDbContext;
        this.catalogService = catalogService;
    }

    public async Task<OneOf<Success<CartProductItem>, ValidationError>> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new ValidationError(validationResult.ToDictionary());
        }

        var product = await catalogService.GetProductAsync(request.ProductId, cancellationToken);

        var cartItem = cartDbContext.CartItems.FirstOrDefault(ci => ci.ProductId == request.ProductId && ci.CustomerId == request.CustomerId && ci.Status == CartItemStatus.InCart);

        if (cartItem is not null)
        {
            cartItem.Quantity += request.Quantity;
        }
        else
        {
            cartItem = request.Adapt<Data.Entities.CartItem>();
            cartItem.ProductName = product.Name;
            cartItem.UnitPrice = product.UnitPrice;
            cartDbContext.CartItems.Add(cartItem);
        }

        if (cartItem.Quantity > product.AvailableStock)
        {
            return new ValidationError("Product is out of stock");
        }

        await cartDbContext.SaveChangesAsync(cancellationToken);

        var cartProductItem = cartItem.Adapt<CartProductItem>();

        return new Success<CartProductItem>(cartProductItem);
    }
}