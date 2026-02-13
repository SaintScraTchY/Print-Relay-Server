using PrintRelayServer.Domain.Base;
using PrintRelayServer.Domain.Entities.Identity;
using PrintRelayServer.Domain.Entities.PrintAgg;

namespace PrintRelayServer.Domain.Entities.AgentAgg;

/// <summary>
/// Represents a client agent application running on a computer/device.
/// One agent can manage multiple printers (USB, Network, etc.)
/// </summary>
public class ClientAgent : TimestampedEntity
{
    public string Name { get; private set; }
    public string MachineId { get; private set; } // Unique identifier for the physical machine
    
    // Owner relationship (optional for now, required when auth is added)
    public Guid? OwnerId { get; private set; }
    public AppUser? Owner { get; set; }
    
    // Runtime connection state (ephemeral - resets on restart)
    public bool IsConnected { get; private set; }
    public string? SignalRConnectionId { get; private set; }
    public DateTime? LastHeartbeat { get; private set; }
    
    // Persistent metadata
    public string? HostName { get; private set; }
    public string? IpAddress { get; private set; }
    public string? Version { get; private set; } // Agent software version
    public string? Platform { get; private set; } // Windows, Linux, macOS, Android, iOS
    
    // Relationships
    public ICollection<AgentDevice> AgentDevices { get; set; } = new List<AgentDevice>();
    public ICollection<PrintJob> AssignedJobs { get; set; } = new List<PrintJob>();
    
    // EF Constructor
    protected ClientAgent() { }
    
    public ClientAgent(string name, string machineId, Guid? ownerId = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Agent name cannot be empty", nameof(name));
        
        if (string.IsNullOrWhiteSpace(machineId))
            throw new ArgumentException("Machine ID cannot be empty", nameof(machineId));
        
        Id = Guid.NewGuid();
        Name = name;
        MachineId = machineId;
        OwnerId = ownerId;
        IsConnected = false;
    }
    
    public void Connect(string connectionId, string? ipAddress = null, string? hostName = null)
    {
        SignalRConnectionId = connectionId;
        IsConnected = true;
        LastHeartbeat = DateTime.UtcNow;
        IpAddress = ipAddress;
        HostName = hostName;
    }
    
    public void Disconnect()
    {
        IsConnected = false;
        SignalRConnectionId = null;
    }
    
    public void UpdateHeartbeat()
    {
        LastHeartbeat = DateTime.UtcNow;
    }
    
    public void UpdateMetadata(string? version = null, string? platform = null)
    {
        if (!string.IsNullOrWhiteSpace(version))
            Version = version;
        
        if (!string.IsNullOrWhiteSpace(platform))
            Platform = platform;
    }
    
    public void Rename(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Agent name cannot be empty", nameof(newName));
        
        Name = newName;
    }
}