using CatalogService.Contracts;
using CatalogService.Data;
using CatalogService.Services;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Queries;

public record ListAllProductsQuery : IRequest<IReadOnlyCollection<Product>>;

public class ListAllProductsHandler : IRequestHandler<ListAllProductsQuery, IReadOnlyCollection<Product>>
{
    private readonly CatalogDbContext catalogDbContext;

    public ListAllProductsHandler(CatalogDbContext catalogDbContext)
    {
        this.catalogDbContext = catalogDbContext;
    }

    public async Task<IReadOnlyCollection<Product>> Handle(ListAllProductsQuery request, CancellationToken cancellationToken)
    {
        var allProducts = await catalogDbContext.Products.ToListAsync(cancellationToken);

        return allProducts.Adapt<List<Product>>();
    }
}