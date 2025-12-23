namespace Pcbuilder.Api.Models;

public class PsuSpec
{
    public Guid PartId { get; set; }
    public Part Part { get; set; } = null!;

    public int Wattage { get; set; }
    public string? Efficiency { get; set; }           // 80+ Gold...
    public bool? Modular { get; set; }
}
