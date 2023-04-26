namespace CustomerService.Data.Entities;

public record Customer
{
    public Guid CustomerId { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
