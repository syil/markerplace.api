using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using OrderService.Contracts;
using OrderService.Data;

namespace OrderService.Queries;

public record GetOrderQuery(Guid OrderId) : IRequest<OneOf<Order, NotFound>>;

public class GetOrderHandler : IRequestHandler<GetOrderQuery, OneOf<Order, NotFound>>
{
    private readonly OrderDbContext orderDbContext;

    public GetOrderHandler(OrderDbContext orderDbContext)
    {
        this.orderDbContext = orderDbContext;
    }

    public async Task<OneOf<Order, NotFound>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await orderDbContext.Orders.Include(o => o.OrderItems).Include(o => o.Buyer)
            .FirstOrDefaultAsync(o => o.OrderId == request.OrderId, cancellationToken);

        if (order is null)
        {
            return new NotFound();
        }

        return order.Adapt<Order>();
    }
}
