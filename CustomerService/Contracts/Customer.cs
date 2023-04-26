namespace CustomerService.Contracts;

public record Customer
{
    public Guid CustomerId { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }
}