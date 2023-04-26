namespace CartService.Services.OrderService.Contracts;

public class Order
{
    public Guid OrderId { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public Buyer Buyer { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}
