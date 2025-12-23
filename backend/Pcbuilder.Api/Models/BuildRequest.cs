namespace Pcbuilder.Api.Models;

public class BuildRequest
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }
    public AppUser? User { get; set; }

    public Guid MarketId { get; set; }
    public Market Market { get; set; } = null!;

    public short UseCaseId { get; set; }
    public UseCase UseCase { get; set; } = null!;

    public decimal BudgetMin { get; set; }
    public decimal BudgetMax { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public ICollection<BuildRecommendation> Recommendations { get; set; } = new List<BuildRecommendation>();
}
