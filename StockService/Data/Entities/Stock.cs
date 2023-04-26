namespace StockService.Data.Entities;

public record Stock
{
    public Guid StockId { get; set; }
    public Guid ProductId { get; set; }
    public int Available { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public uint Version { get; set; }
}
