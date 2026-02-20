using PrintRelayServer.Domain.Entities.AgentAgg;

namespace PrintRelayServer.Application.Application.Interfaces;

/// <summary>
/// Service for managing connected client agents.
/// Tracks which agents are online and can handle print jobs.
/// </summary>
public interface IAgentRegistryService
{
    /// <summary>
    /// Register a new agent connection
    /// </summary>
    /// <param name="agentName">Friendly name</param>
    /// <param name="machineId">Unique machine identifier</param>
    /// <param name="connectionId">SignalR connection ID</param>
    /// <param name="deviceIds">Devices this agent can access</param>
    /// <returns>Agent entity</returns>
    Task<ClientAgent> RegisterAgentAsync(
        string agentName,
        string machineId,
        string connectionId,
        List<Guid> deviceIds);
    
    /// <summary>
    /// Mark agent as disconnected
    /// </summary>
    Task UnregisterAgentAsync(string connectionId);
    
    /// <summary>
    /// Update agent heartbeat timestamp
    /// </summary>
    Task UpdateHeartbeatAsync(string connectionId);
    
    /// <summary>
    /// Find an available agent that can handle a specific device
    /// </summary>
    /// <returns>Agent or null if none available</returns>
    Task<ClientAgent?> GetAvailableAgentForDeviceAsync(Guid deviceId);
    
    /// <summary>
    /// Get agent by connection ID
    /// </summary>
    Task<ClientAgent?> GetAgentByConnectionIdAsync(string connectionId);
    
    /// <summary>
    /// Get all connected agents
    /// </summary>
    Task<List<ClientAgent>> GetConnectedAgentsAsync();
}