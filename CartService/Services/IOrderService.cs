using CartService.Services.OrderService.Contracts;

namespace CartService.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(CreateOrder createOrder, CancellationToken cancellationToken = default);
    }
}