namespace Pcbuilder.Api.Models;

public class Build
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public Guid? CreatedById { get; set; }
    public AppUser? CreatedBy { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public decimal TotalPrice { get; set; } = 0;
    public int EstPowerW { get; set; } = 0;
    public bool PowerSafe { get; set; } = true;
    public decimal PerfScore { get; set; } = 0;
    public decimal UpgradabilityScore { get; set; } = 0;

    public ICollection<BuildPart> BuildParts { get; set; } = new List<BuildPart>();
    public ICollection<BuildWarning> Warnings { get; set; } = new List<BuildWarning>();
}
