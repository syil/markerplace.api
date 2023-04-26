using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Commands;
using OrderService.Queries;

namespace OrderService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : Controller
{
    private readonly IMediator mediator;

    public OrdersController(IMediator mediator)
	{
        this.mediator = mediator;
    }

    [HttpGet("{orderId:guid}")]
    public async Task<IActionResult> Get(Guid orderId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetOrderQuery(orderId), cancellationToken);

        return result.Match<IActionResult>(Ok, _ => NotFound());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderCommand createOrderCommand, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(createOrderCommand, cancellationToken);

        return result.Match(
            success => CreatedAtAction(nameof(Get), new { orderId = success.Value.OrderId }, success.Value),
            invalid => ValidationProblem(new ValidationProblemDetails(invalid.ErrorMessages)));
    }

    [HttpGet("customer/{customerId:guid}")]
    public async Task<IActionResult> CustomerOrders(Guid customerId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new ListCustomerOrdersQuery(customerId), cancellationToken);

        return Ok(result);
    }
}
