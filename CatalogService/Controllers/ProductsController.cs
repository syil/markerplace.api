using CatalogService.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : Controller
{
    private readonly IMediator mediator;

    public ProductsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("{productId:guid}")]
    public async Task<IActionResult> Get(Guid productId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetProductQuery(productId), cancellationToken);

        return result.Match<IActionResult>(Ok, _ => NotFound());
    }

    [HttpGet]
    public async Task<IActionResult> ListAll(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new ListAllProductsQuery(), cancellationToken);

        return Ok(result);
    }
}
