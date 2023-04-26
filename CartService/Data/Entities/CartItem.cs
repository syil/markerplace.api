using CartService.Data.Enums;

namespace CartService.Data.Entities;

public record CartItem
{
    public Guid CartItemId { get; set; }
    public Guid ProductId { get; set; }
    public Guid CustomerId { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public CartItemStatus Status { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
