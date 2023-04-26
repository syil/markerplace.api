namespace CatalogService.Data.Entities;

public record Product
{
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public decimal UnitPrice { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
