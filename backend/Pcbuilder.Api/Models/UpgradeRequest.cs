namespace Pcbuilder.Api.Models;

public class UpgradeRequest
{
    public Guid Id { get; set; }

    public Guid UserPcId { get; set; }
    public UserPc UserPc { get; set; } = null!;

    public Guid MarketId { get; set; }
    public Market Market { get; set; } = null!;

    public decimal? BudgetMax { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public ICollection<UpgradePlan> Plans { get; set; } = new List<UpgradePlan>();
}
