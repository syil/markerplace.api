using OrderService.Services.CustomerService.Contracts;

namespace OrderService.Services;

public interface ICustomerService
{
    Task<Customer> GetCustomerAsync(Guid customerId, CancellationToken cancellationToken = default);
}
