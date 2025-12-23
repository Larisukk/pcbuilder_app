namespace Pcbuilder.Api.Models;

public class BuildRecommendation
{
    public Guid Id { get; set; }

    public Guid RequestId { get; set; }
    public BuildRequest Request { get; set; } = null!;

    public Guid BuildId { get; set; }
    public Build Build { get; set; } = null!;

    public int Rank { get; set; }

    public decimal PerformanceScore { get; set; }
    public decimal PriceEfficiency { get; set; }       // perf/price
    public decimal Upgradability { get; set; }
    public decimal PowerSafetyScore { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}
