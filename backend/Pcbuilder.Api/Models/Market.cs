namespace Pcbuilder.Api.Models;

public class Market
{
    public Guid Id { get; set; }
    public string Code { get; set; } = null!;          // e.g. "RO"
    public string Name { get; set; } = null!;
    public string CurrencyCode { get; set; } = null!;  // e.g. "RON"
    public DateTimeOffset CreatedAt { get; set; }
}
