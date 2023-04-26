namespace CartService.Services.OrderService.Contracts;

public record CreateOrder
{
    public Guid CustomerId { get; set; }
    public List<OrderItem> OrderItems { get; set; }

    public record OrderItem(Guid ProductId, int Quantity);
}
