namespace Pcbuilder.Api.Models;

public class BuildWarning
{
    public Guid Id { get; set; }

    public Guid BuildId { get; set; }
    public Build Build { get; set; } = null!;

    public string Code { get; set; } = null!;      // PSU_INSUFFICIENT, SOCKET_MISMATCH...
    public string Message { get; set; } = null!;
    public string Severity { get; set; } = "WARN"; // INFO/WARN/ERROR

    public DateTimeOffset CreatedAt { get; set; }
}
