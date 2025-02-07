namespace PrintRelayServer.Shared.Contracts.Users;

public class GetUser
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}