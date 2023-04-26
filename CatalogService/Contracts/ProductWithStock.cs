namespace CatalogService.Contracts;

public class ProductWithStock
{
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public decimal UnitPrice { get; set; }
    public int AvailableStock { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
