using PrintRelayServer.Domain.Base;
using PrintRelayServer.Domain.Entities.Identity;

namespace PrintRelayServer.Domain.Entities.ManagementAgg;

public class UserTeam : AuditEntity
{
    public Guid MemberId { get; set; }
    public AppUser Member { get; set; }
    
    public Guid TeamId { get; set; }
    public Team Team { get; set; }
    
    public DateTime JoinedOn { get; set; }
}
