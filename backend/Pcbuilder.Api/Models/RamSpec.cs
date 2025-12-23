namespace Pcbuilder.Api.Models;

public class RamSpec
{
    public Guid PartId { get; set; }
    public Part Part { get; set; } = null!;

    public string MemoryType { get; set; } = null!;   // DDR4, DDR5
    public int CapacityGb { get; set; }               // kit total
    public int Sticks { get; set; }
    public int? SpeedMhz { get; set; }
}
