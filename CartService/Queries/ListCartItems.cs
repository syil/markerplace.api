using CartService.Contracts;
using CartService.Data;
using CartService.Data.Enums;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CartService.Queries;

public record ListCartItemsQuery(Guid CustomerId) : IRequest<IReadOnlyCollection<CartProductItem>>;

public class ListCartItemsHandler : IRequestHandler<ListCartItemsQuery, IReadOnlyCollection<CartProductItem>>
{
    private readonly CartDbContext cartDbContext;

    public ListCartItemsHandler(CartDbContext cartDbContext)
    {
        this.cartDbContext = cartDbContext;
    }

    public async Task<IReadOnlyCollection<CartProductItem>> Handle(ListCartItemsQuery request, CancellationToken cancellationToken)
    {
        var items = await cartDbContext.CartItems.Where(ci => ci.CustomerId == request.CustomerId && ci.Status == CartItemStatus.InCart)
            .ToListAsync(cancellationToken);

        return items.Adapt<List<CartProductItem>>();
    }
}
