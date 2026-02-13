namespace PrintRelayServer.Domain.Entities.Identity;

public class ClientAgent
{
    public Guid Id { get; set; }
    public string Name { get; set; }              // "Office PC - John's Desk"
    public string MachineId { get; set; }         // Unique per computer (could use MAC address hash)
    
    public Guid? OwnerId { get; set; }            // Which user "owns" this agent (optional for now)
    public AppUser? Owner { get; set; }
    
    // Runtime state (ephemeral, resets on disconnect)
    public bool IsConnected { get; set; }
    public string? SignalRConnectionId { get; set; }
    public DateTime? LastHeartbeat { get; set; }
    
    // Persistent metadata
    public DateTime RegisteredAt { get; set; }
    public string? HostName { get; set; }
    public string? IpAddress { get; set; }
    public string? Version { get; set; }           // Agent software version
    
    // Relationships
    public ICollection<AgentDevice> AgentDevices { get; set; }  // ← Join table!
}