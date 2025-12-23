namespace Pcbuilder.Api.Models;

public class PartType
{
    public short Id { get; set; }      // smallint
    public string Code { get; set; } = null!; // CPU, GPU, ...
}
