using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockService.Queries;

namespace StockService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StocksController : Controller
{
    private readonly IMediator mediator;

    public StocksController(IMediator mediator)
	{
        this.mediator = mediator;
    }

    [HttpGet("available/{productId:guid}")]
    public async Task<IActionResult> Available(Guid productId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAvailableStockQuery(productId), cancellationToken);

        return result.Match<IActionResult>(
            value => Ok(value),
            _ => NotFound());
    }
}
