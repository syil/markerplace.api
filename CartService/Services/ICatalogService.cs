using CartService.Services.CatalogService.Contracts;

namespace CartService.Services;

public interface ICatalogService
{
    Task<Product> GetProductAsync(Guid productId, CancellationToken cancellationToken = default);
}
