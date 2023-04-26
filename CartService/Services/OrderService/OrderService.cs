using CartService.Services.OrderService.Contracts;

namespace CartService.Services.OrderService;

public class OrderService : IOrderService
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly ILogger<OrderService> logger;
    private readonly string? baseUrl;

    public OrderService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<OrderService> logger)
    {
        this.httpClientFactory = httpClientFactory;
        this.logger = logger;
        this.baseUrl = configuration["Services:OrderService"];
    }

    public async Task<Order> CreateOrderAsync(CreateOrder createOrder, CancellationToken cancellationToken = default)
    {
        try
        {
            var httpClient = httpClientFactory.CreateClient(nameof(IOrderService));
            var response = await httpClient.PostAsJsonAsync($"{baseUrl}/api/orders", createOrder, cancellationToken);

            return await response.Content.ReadFromJsonAsync<Order>(cancellationToken: cancellationToken);
        }
        catch (Exception exc)
        {
            logger.LogError(exc, "Creating order for customer {CustomerId} failed", createOrder.CustomerId);
            throw new ApplicationException("Creating order failed", exc);
        }
    }
}
