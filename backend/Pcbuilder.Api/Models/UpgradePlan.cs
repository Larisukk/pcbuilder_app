namespace Pcbuilder.Api.Models;

public class UpgradePlan
{
    public Guid Id { get; set; }

    public Guid RequestId { get; set; }
    public UpgradeRequest Request { get; set; } = null!;

    public Guid FromBuildId { get; set; }
    public Build FromBuild { get; set; } = null!;

    public Guid ToBuildId { get; set; }
    public Build ToBuild { get; set; } = null!;

    public int Rank { get; set; }

    public decimal FpsGain { get; set; } = 0;
    public decimal Cost { get; set; } = 0;
    public decimal FpsGainPerCost { get; set; } = 0;
    public decimal PowerSafetyScore { get; set; } = 0;
    public decimal ResaleValueEst { get; set; } = 0;

    public DateTimeOffset CreatedAt { get; set; }

    public ICollection<UpgradePlanChange> Changes { get; set; } = new List<UpgradePlanChange>();
}
