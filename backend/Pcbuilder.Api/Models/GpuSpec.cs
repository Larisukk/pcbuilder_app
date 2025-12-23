namespace Pcbuilder.Api.Models;

public class GpuSpec
{
    public Guid PartId { get; set; }
    public Part Part { get; set; } = null!;

    public int VramGb { get; set; }
    public int? LengthMm { get; set; }
    public int? PcieGen { get; set; }
    public int? RecommendedPsuWatts { get; set; }
}
