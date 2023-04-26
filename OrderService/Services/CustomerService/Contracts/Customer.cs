namespace OrderService.Services.CustomerService.Contracts;

public class Customer
{
    public Guid CustomerId { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }
}
