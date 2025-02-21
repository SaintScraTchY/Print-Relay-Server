namespace PrintRelayServer.Shared.Contracts.Users;

public class GetUserFilter
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? UserName { get; set; }
    public IEnumerable<Guid>? Roles { get; set; }
}