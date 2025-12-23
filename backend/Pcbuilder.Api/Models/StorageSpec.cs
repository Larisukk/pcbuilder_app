namespace Pcbuilder.Api.Models;

public class StorageSpec
{
    public Guid PartId { get; set; }
    public Part Part { get; set; } = null!;

    public string Kind { get; set; } = null!;         // NVME, SATA_SSD, HDD
    public int CapacityGb { get; set; }
    public string? Interface { get; set; }            // PCIe 4.0 x4, SATA...
    public int? SeqReadMbS { get; set; }
}
