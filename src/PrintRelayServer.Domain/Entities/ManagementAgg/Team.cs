using PrintRelayServer.Domain.Base;
using PrintRelayServer.Domain.Entities.Identity;

namespace PrintRelayServer.Domain.Entities.ManagementAgg;

public class Team : FullEntity
{
    public string Name { get; set; }
    public string Description { get; set; }

    public Guid LeaderId { get; set; }
    public AppUser Leader { get; set; }
    public ICollection<UserTeam> Memebers { get; set; }
}