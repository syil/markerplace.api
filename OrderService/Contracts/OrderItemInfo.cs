namespace OrderService.Contracts;

public class OrderItemInfo
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
