namespace Pcbuilder.Api.Models;

public class Vendor
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Website { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
