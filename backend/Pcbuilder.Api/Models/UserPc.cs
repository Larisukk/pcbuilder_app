namespace Pcbuilder.Api.Models;

public class UserPc
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public AppUser User { get; set; } = null!;

    public string Name { get; set; } = "My PC";

    public Guid BuildId { get; set; }
    public Build Build { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }

    public ICollection<UpgradeRequest> UpgradeRequests { get; set; } = new List<UpgradeRequest>();
}
