using CustomerService.Contracts;
using CustomerService.Data;
using Mapster;
using MediatR;
using OneOf;
using OneOf.Types;

namespace CustomerService.Queries;

public record GetCustomerQuery(Guid CustomerId) : IRequest<OneOf<Customer, NotFound>>;

public class GetCustomerHandler : IRequestHandler<GetCustomerQuery, OneOf<Customer, NotFound>>
{
    private readonly CustomerDbContext customerDbContext;

    public GetCustomerHandler(CustomerDbContext customerDbContext)
    {
        this.customerDbContext = customerDbContext;
    }

    public async Task<OneOf<Customer, NotFound>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        var found = await customerDbContext.Customers.FindAsync(new object[] { request.CustomerId }, cancellationToken);

        if (found == null)
        {
            return new NotFound();
        }
        else
        {
            return found.Adapt<Customer>();
        }
    }
}