using CatalogService.Contracts;
using CatalogService.Data;
using CatalogService.Services;
using Mapster;
using MediatR;
using OneOf;
using OneOf.Types;

namespace CatalogService.Queries;

public record GetProductQuery(Guid ProductId) : IRequest<OneOf<ProductWithStock, NotFound>>;

public class GetProductHandler : IRequestHandler<GetProductQuery, OneOf<ProductWithStock, NotFound>>
{
    private readonly CatalogDbContext catalogDbContext;
    private readonly IStockService stockService;

    public GetProductHandler(CatalogDbContext catalogDbContext, IStockService stockService)
    {
        this.catalogDbContext = catalogDbContext;
        this.stockService = stockService;
    }

    public async Task<OneOf<ProductWithStock, NotFound>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await catalogDbContext.Products.FindAsync(new object[] { request.ProductId }, cancellationToken: cancellationToken);

        if (product is null)
        {
            return new NotFound();
        }

        int availableStock = await stockService.GetAvailableStockAsync(request.ProductId, cancellationToken);
        var productWithStock = product.Adapt<ProductWithStock>();

        productWithStock.AvailableStock = availableStock;

        return productWithStock;
    }
}