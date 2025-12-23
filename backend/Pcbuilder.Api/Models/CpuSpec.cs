namespace Pcbuilder.Api.Models;

public class CpuSpec
{
    public Guid PartId { get; set; }
    public Part Part { get; set; } = null!;

    public string Socket { get; set; } = null!;
    public int Cores { get; set; }
    public int Threads { get; set; }
    public decimal? BaseGhz { get; set; }
    public decimal? BoostGhz { get; set; }
    public bool Igpu { get; set; } = false;
}
