namespace Pcbuilder.Api.Models;

public class CoolerSpec
{
    public Guid PartId { get; set; }
    public Part Part { get; set; } = null!;

    public string Kind { get; set; } = null!;         // AIR, AIO
    public int? MaxTdpWatts { get; set; }
    public int? HeightMm { get; set; }
    public string? SocketSupport { get; set; }        // "AM5,LGA1700"
}
