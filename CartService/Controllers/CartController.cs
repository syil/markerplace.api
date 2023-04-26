using CartService.Commands;
using CartService.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CartService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController : Controller
{
    private readonly IMediator mediator;

    public CartController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("{shoppingCartItemId:guid}")]
    public async Task<IActionResult> Item(Guid shoppingCartItemId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetCartItemQuery(shoppingCartItemId), cancellationToken);

        return result.Match<IActionResult>(Ok, _ => NotFound());
    }

    [HttpGet("list/{customerId:guid}")]
    public async Task<IActionResult> List(Guid customerId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new ListCartItemsQuery(customerId), cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddCartItemCommand addCartItemCommand, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(addCartItemCommand, cancellationToken);

        return result.Match(
            success => CreatedAtAction(nameof(Item), new { shoppingCartItemId = success.Value.CartItemId }, success.Value),
            invalid => ValidationProblem(new ValidationProblemDetails(invalid.ErrorMessages)));
    }

    [HttpPut("{shoppingCartItemId:guid}")]
    public async Task<IActionResult> Update(Guid shoppingCartItemId, UpdateCartItemCommand updateCartItemCommand, CancellationToken cancellationToken)
    {
        updateCartItemCommand.ShoppingCartItemId = shoppingCartItemId;
        var result = await mediator.Send(updateCartItemCommand, cancellationToken);

        return result.Match(
            _ => Ok(),
            _ => NotFound(),
            invalid => ValidationProblem(new ValidationProblemDetails(invalid.ErrorMessages)));
    }

    [HttpPost("makeorder")]
    public async Task<IActionResult> MakeOrder(MakeOrderCommand makeOrderCommand, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(makeOrderCommand, cancellationToken);

        return result.Match<IActionResult>(
            success => Ok(success.Value),
            error => BadRequest(error.Value));
    }
}
