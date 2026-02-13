using PrintRelayServer.Domain.Base;
using PrintRelayServer.Domain.Base.Contracts;
using PrintRelayServer.Domain.Entities.DeviceAgg;

namespace PrintRelayServer.Domain.Entities.AgentAgg;

/// <summary>
/// Join table representing which printers an agent can access.
/// One agent can manage multiple printers.
/// One printer can be accessible by multiple agents (redundancy).
/// </summary>
public class AgentDevice : ActivatableEntity
{
    public Guid AgentId { get; private set; }
    public ClientAgent Agent { get; set; }
    
    public Guid DeviceId { get; private set; }
    public Device Device { get; set; }
    
    public bool IsActive { get; private set; }
    public DateTime LinkedAt { get; private set; }
    public DateTime? LastUsedAt { get; private set; }
    
    // EF Constructor
    protected AgentDevice() { }
    
    public AgentDevice(Guid agentId, Guid deviceId)
    {
        Id = Guid.NewGuid();
        AgentId = agentId;
        DeviceId = deviceId;
        IsActive = true;
        LinkedAt = DateTime.UtcNow;
    }
    
    public void MarkAsUsed()
    {
        LastUsedAt = DateTime.UtcNow;
    }
}