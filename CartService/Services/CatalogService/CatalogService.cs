using CartService.Services.CatalogService.Contracts;

namespace CartService.Services.CatalogService;

public class CatalogService : ICatalogService
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly ILogger<CatalogService> logger;
    private readonly string? baseUrl;

    public CatalogService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<CatalogService> logger)
    {
        this.httpClientFactory = httpClientFactory;
        this.logger = logger;
        this.baseUrl = configuration["Services:CatalogService"];
    }

    public async Task<Product> GetProductAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        try
        {
            var client = httpClientFactory.CreateClient(nameof(ICatalogService));
            var result = await client.GetFromJsonAsync<Product>($"{baseUrl}/api/products/{productId}", cancellationToken);

            return result;
        }
        catch (Exception exc)
        {
            logger.LogError(exc, "Getting product {ProductId} failed", productId);
            throw new ApplicationException("Getting product failed", exc);
        }
    }
}
