using OrderService.Services.CustomerService.Contracts;

namespace OrderService.Services.CustomerService;

public class CustomerService : ICustomerService
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly ILogger<CustomerService> logger;
    private readonly string? baseUrl;

    public CustomerService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<CustomerService> logger)
    {
        this.httpClientFactory = httpClientFactory;
        this.logger = logger;
        this.baseUrl = configuration["Services:CustomerService"];
    }

    public async Task<Customer> GetCustomerAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        try
        {
            var httpClient = httpClientFactory.CreateClient(nameof(ICustomerService));
            var result = await httpClient.GetFromJsonAsync<Customer>($"{baseUrl}/api/customers/{customerId}", cancellationToken);

            return result;
        }
        catch (Exception exc)
        {
            logger.LogError(exc, "Getting customer {CustomerId} failed", customerId);
            throw new ApplicationException("Getting customer failed", exc);
        }
    }
}
