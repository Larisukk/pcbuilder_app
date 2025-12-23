namespace Pcbuilder.Api.Models;

public class UpgradePlanChange
{
    public Guid PlanId { get; set; }
    public UpgradePlan Plan { get; set; } = null!;

    public Guid PartId { get; set; }
    public Part Part { get; set; } = null!;

    public string ChangeType { get; set; } = null!; // ADD or REMOVE
    public int Quantity { get; set; } = 1;
}
