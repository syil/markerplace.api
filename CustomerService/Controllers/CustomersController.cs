using CustomerService.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : Controller
{
    private readonly IMediator mediator;

    public CustomersController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("{customerId:guid}")]
    public async Task<IActionResult> Get(Guid customerId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetCustomerQuery(customerId), cancellationToken);

        return result.Match<IActionResult>(Ok, _ => NotFound());
    }
}
