namespace CartService.Contracts;

public record CartProductItem
{
    public Guid CartItemId { get; set; }
    public Guid ProductId { get; set; }
    public Guid CustomerId { get; set; }
    public int Quantity { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public decimal UnitPrice { get; set; }
    public string ProductName { get; set; }
    public decimal TotalPrice => UnitPrice * Quantity;
}
