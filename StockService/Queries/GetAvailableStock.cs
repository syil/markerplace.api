using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using StockService.Data;

namespace StockService.Queries;

public record GetAvailableStockQuery(Guid ProductId) : IRequest<OneOf<int, NotFound>>;

public class GetAvailableStockHandler : IRequestHandler<GetAvailableStockQuery, OneOf<int, NotFound>>
{
    private readonly StockDbContext stockDbContext;

    public GetAvailableStockHandler(StockDbContext stockDbContext)
    {
        this.stockDbContext = stockDbContext;
    }

    public async Task<OneOf<int, NotFound>> Handle(GetAvailableStockQuery request, CancellationToken cancellationToken)
    {
        var stock = await stockDbContext.Stocks.FirstOrDefaultAsync(s => s.ProductId == request.ProductId, cancellationToken);

        if (stock is null)
        {
            return new NotFound();
        }

        return stock.Available;
    }
}