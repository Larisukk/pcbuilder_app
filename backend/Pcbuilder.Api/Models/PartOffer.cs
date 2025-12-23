namespace Pcbuilder.Api.Models;

public class PartOffer
{
    public Guid Id { get; set; }

    public Guid PartId { get; set; }
    public Part Part { get; set; } = null!;

    public Guid VendorId { get; set; }
    public Vendor Vendor { get; set; } = null!;

    public Guid MarketId { get; set; }
    public Market Market { get; set; } = null!;

    public string? Url { get; set; }
    public decimal Price { get; set; }
    public string CurrencyCode { get; set; } = null!;
    public bool InStock { get; set; } = true;
    public DateTimeOffset LastChecked { get; set; }
}
