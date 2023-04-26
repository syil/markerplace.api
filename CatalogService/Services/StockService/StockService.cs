using Microsoft.Extensions.Caching.Distributed;

namespace CatalogService.Services.StockService;

public class StockService : IStockService
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly ILogger<StockService> logger;
    private readonly string? baseUrl;

    public StockService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<StockService> logger)
    {
        this.httpClientFactory = httpClientFactory;
        this.logger = logger;
        this.baseUrl = configuration["Services:StockService"];
    }

    public async Task<int> GetAvailableStockAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        try
        {
            var httpClient = httpClientFactory.CreateClient(nameof(IStockService));
            int result = await httpClient.GetFromJsonAsync<int>($"{baseUrl}/api/stocks/available/{productId}", cancellationToken);

            return result;
        }
        catch (Exception exc)
        {
            logger.LogError(exc, "Checking stock for {ProductId} failed", productId);
            throw new ApplicationException("Checking stock failed", exc);
        }
    }
}
