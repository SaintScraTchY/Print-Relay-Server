using PrintRelayServer.Domain.Base;
using PrintRelayServer.Domain.Entities.Identity;

namespace PrintRelayServer.Domain.Entities.ManagementAgg;

public class Team : AuditableEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }

    public Guid LeaderId { get; set; }
    public AppUser Leader { get; set; }

    public ICollection<TeamMember> Members { get; set; } = new HashSet<TeamMember>();

    public ICollection<TeamPermissionGrant> TeamPermissionGrants { get; set; } = new HashSet<TeamPermissionGrant>();

    protected Team()
    {
        
    }

    public Team(string name, string description)
    {
        Name = name;
        Description = description;
    }
}