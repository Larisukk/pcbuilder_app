namespace Pcbuilder.Api.Models;

public class BuildActivity
{
    public Guid Id { get; set; }

    public Guid? MarketId { get; set; }
    public Market? Market { get; set; }

    public short? UseCaseId { get; set; }
    public UseCase? UseCase { get; set; }

    public Guid? BuildId { get; set; }
    public Build? Build { get; set; }

    public Guid? UserId { get; set; }
    public AppUser? User { get; set; }

    public string Action { get; set; } = null!; // REQUESTED, SAVED, UPGRADED
    public DateTimeOffset CreatedAt { get; set; }
}
