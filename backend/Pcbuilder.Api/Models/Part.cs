namespace Pcbuilder.Api.Models;

public class Part
{
    public Guid Id { get; set; }

    public short TypeId { get; set; }
    public PartType Type { get; set; } = null!;

    public string Brand { get; set; } = null!;
    public string Model { get; set; } = null!;
    public string Name { get; set; } = null!;

    public decimal? Msrp { get; set; }
    public int? ReleaseYear { get; set; }
    public bool IsActive { get; set; } = true;

    // scoring / validation helpers
    public decimal PerfScore { get; set; } = 0;
    public int? TdpWatts { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    // One-to-one optional spec tables (depending on TypeId)
    public CpuSpec? CpuSpec { get; set; }
    public GpuSpec? GpuSpec { get; set; }
    public MotherboardSpec? MotherboardSpec { get; set; }
    public RamSpec? RamSpec { get; set; }
    public StorageSpec? StorageSpec { get; set; }
    public PsuSpec? PsuSpec { get; set; }
    public CaseSpec? CaseSpec { get; set; }
    public CoolerSpec? CoolerSpec { get; set; }
}
