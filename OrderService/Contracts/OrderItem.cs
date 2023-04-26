namespace OrderService.Contracts;

public class OrderItem
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
}
