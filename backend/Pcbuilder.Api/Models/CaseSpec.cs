namespace Pcbuilder.Api.Models;

public class CaseSpec
{
    public Guid PartId { get; set; }
    public Part Part { get; set; } = null!;

    public string FormFactorSupport { get; set; } = null!; // "ATX,mATX,ITX"
    public int? MaxGpuLengthMm { get; set; }
    public int? MaxCoolerHeightMm { get; set; }
}
