namespace CatalogService.Services;

public interface IStockService
{
    Task<int> GetAvailableStockAsync(Guid productId, CancellationToken cancellationToken = default);
}
