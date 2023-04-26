using CartService.Contracts;
using CartService.Data;
using Mapster;
using MediatR;
using OneOf;
using OneOf.Types;

namespace CartService.Queries;

public record GetCartItemQuery(Guid ShoppingCartItemId) : IRequest<OneOf<CartProductItem, NotFound>>;

public class GetCartItemHandler : IRequestHandler<GetCartItemQuery, OneOf<CartProductItem, NotFound>>
{
    private readonly CartDbContext cartDbContext;

    public GetCartItemHandler(CartDbContext cartDbContext)
    {
        this.cartDbContext = cartDbContext;
    }

    public async Task<OneOf<CartProductItem, NotFound>> Handle(GetCartItemQuery request, CancellationToken cancellationToken)
    {
        var found = await cartDbContext.CartItems.FindAsync(new object[] { request.ShoppingCartItemId }, cancellationToken);

        if (found is null)
        {
            return new NotFound();
        }
        else
        {
            return found.Adapt<CartProductItem>();
        }
    }
}
