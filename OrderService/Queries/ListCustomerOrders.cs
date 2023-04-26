using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Contracts;
using OrderService.Data;

namespace OrderService.Queries;

public record ListCustomerOrdersQuery(Guid CustomerId) : IRequest<IReadOnlyCollection<Order>>;

public class ListCustomerOrdersHandler : IRequestHandler<ListCustomerOrdersQuery, IReadOnlyCollection<Order>>
{
    private readonly OrderDbContext orderDbContext;

    public ListCustomerOrdersHandler(OrderDbContext orderDbContext)
	{
        this.orderDbContext = orderDbContext;
    }

    public async Task<IReadOnlyCollection<Order>> Handle(ListCustomerOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await orderDbContext.Orders.Include(o => o.OrderItems).Include(o => o.Buyer)
            .Where(o => o.CustomerId == request.CustomerId).ToListAsync(cancellationToken);

        return orders.Adapt<List<Order>>();
    }
}