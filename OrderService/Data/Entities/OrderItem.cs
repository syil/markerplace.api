namespace OrderService.Data.Entities;

public class OrderItem
{
    public Guid OrderItemId { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public virtual Order Order { get; set; }
}
