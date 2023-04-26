using CartService.Data;
using CartService.Data.Enums;
using CartService.Services;
using CartService.Services.OrderService.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;

namespace CartService.Commands;

public record MakeOrderCommand(Guid CustomerId) : IRequest<OneOf<Success<Guid>, Error<string>>>;

public class MakeOrderHandler : IRequestHandler<MakeOrderCommand, OneOf<Success<Guid>, Error<string>>>
{
    private readonly CartDbContext cartDbContext;
    private readonly IOrderService orderService;

    public MakeOrderHandler(CartDbContext cartDbContext, IOrderService orderService)
    {
        this.cartDbContext = cartDbContext;
        this.orderService = orderService;
    }

    public async Task<OneOf<Success<Guid>, Error<string>>> Handle(MakeOrderCommand request, CancellationToken cancellationToken)
    {
        var customerCartItems = await cartDbContext.CartItems.Where(ci => ci.CustomerId == request.CustomerId && ci.Status == CartItemStatus.InCart).ToListAsync(cancellationToken);

        if (customerCartItems.Count == 0)
        {
            return new Error<string>("Customer's shoopping cart is empty");
        }

        var createOrder = new CreateOrder
        {
            CustomerId = request.CustomerId,
            OrderItems = customerCartItems.Select(ci => new CreateOrder.OrderItem(ci.ProductId, ci.Quantity)).ToList()
        };

        var createdOrder = await orderService.CreateOrderAsync(createOrder, cancellationToken);

        foreach (var item in customerCartItems)
        {
            item.Status = CartItemStatus.Ordered;
        }

        await cartDbContext.SaveChangesAsync(cancellationToken);

        return new Success<Guid>(createdOrder.OrderId);
    }
}
