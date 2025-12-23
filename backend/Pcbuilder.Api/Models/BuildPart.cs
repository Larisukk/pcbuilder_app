namespace Pcbuilder.Api.Models;

public class BuildPart
{
    public Guid BuildId { get; set; }
    public Build Build { get; set; } = null!;

    public Guid PartId { get; set; }
    public Part Part { get; set; } = null!;

    public int Quantity { get; set; } = 1;
}
