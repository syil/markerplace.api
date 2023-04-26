namespace OrderService.Data.Entities;

public class Buyer
{
    public Guid BuyerId { get; set; }
    public Guid CustomerId { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public virtual ICollection<Order> Orders { get; set; }
}
