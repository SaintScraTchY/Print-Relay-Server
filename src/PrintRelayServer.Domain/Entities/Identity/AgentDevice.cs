using PrintRelayServer.Domain.Entities.DeviceAgg;

namespace PrintRelayServer.Domain.Entities.Identity;

public class AgentDevice
{
    public Guid AgentId { get; set; }
    public ClientAgent Agent { get; set; }
    
    public Guid DeviceId { get; set; }
    public Device Device { get; set; }
    
    public bool IsActive { get; set; }             // Can this agent currently handle jobs for this printer?
    public DateTime LinkedAt { get; set; }
}