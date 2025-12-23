namespace Pcbuilder.Api.Models;

public class AppUser
{
    public Guid Id { get; set; }
    public string Email { get; set; } = null!; // required + unique
    public string? DisplayName { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
