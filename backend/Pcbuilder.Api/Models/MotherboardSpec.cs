namespace Pcbuilder.Api.Models;

public class MotherboardSpec
{
    public Guid PartId { get; set; }
    public Part Part { get; set; } = null!;

    public string Socket { get; set; } = null!;
    public string? Chipset { get; set; }
    public string FormFactor { get; set; } = null!;   // ATX, mATX, ITX
    public string MemoryType { get; set; } = null!;   // DDR4, DDR5
    public int MemorySlots { get; set; }
    public int? MaxMemoryGb { get; set; }
    public int M2Slots { get; set; } = 0;
    public int SataPorts { get; set; } = 0;
}
