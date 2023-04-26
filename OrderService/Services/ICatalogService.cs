using OrderService.Services.CatalogService.Contracts;

namespace OrderService.Services;

public interface ICatalogService
{
    Task<Product> GetProductAsync(Guid productId, CancellationToken cancellationToken = default);
}
