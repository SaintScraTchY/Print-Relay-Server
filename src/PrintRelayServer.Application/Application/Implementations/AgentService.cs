using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PrintRelayServer.Application.Application.Interfaces;
using PrintRelayServer.Domain.Entities.AgentAgg;
using PrintRelayServer.Infrastructure.Contexts;
using PrintRelayServer.Infrastructure.QueryExtensions;

namespace PrintRelayServer.Application.Application.Implementations;

/// <summary>
/// Service for managing connected client agents.
/// Handles registration, heartbeat tracking, and device assignment.
/// </summary>
public class AgentRegistryService : IAgentRegistryService
{
    private readonly PrintRelayContext _db;
    private readonly ILogger<AgentRegistryService> _logger;
    
    public AgentRegistryService(
        PrintRelayContext db,
        ILogger<AgentRegistryService> logger)
    {
        _db = db;
        _logger = logger;
    }
    
    public async Task<ClientAgent> RegisterAgentAsync(
        string agentName,
        string machineId,
        string connectionId,
        List<Guid> deviceIds)
    {
        // Check if agent with this machineId already exists
        var existingAgent = await _db.ClientAgents
            .ByMachineId(machineId)
            .FirstOrDefaultAsync();
        
        ClientAgent agent;
        
        if (existingAgent != null)
        {
            // Reconnecting agent - update connection info
            agent = existingAgent;
            agent.Connect(connectionId);
            
            _logger.LogInformation(
                "Agent {AgentName} ({MachineId}) reconnected with connection {ConnectionId}",
                agentName, machineId, connectionId);
        }
        else
        {
            // New agent - create
            agent = new ClientAgent(agentName, machineId);
            agent.Connect(connectionId);
            _db.ClientAgents.Add(agent);
            
            _logger.LogInformation(
                "New agent {AgentName} ({MachineId}) registered with connection {ConnectionId}",
                agentName, machineId, connectionId);
        }
        
        await _db.SaveChangesAsync();
        
        // Link devices to this agent
        await LinkDevicesToAgentAsync(agent.Id, deviceIds);
        
        return agent;
    }
    
    public async Task UnregisterAgentAsync(string connectionId)
    {
        var agent = await _db.ClientAgents
            .ByConnectionId(connectionId)
            .FirstOrDefaultAsync();
        
        if (agent == null)
        {
            _logger.LogWarning(
                "Attempted to unregister unknown connection {ConnectionId}",
                connectionId);
            return;
        }
        
        agent.Disconnect();
        await _db.SaveChangesAsync();
        
        _logger.LogInformation(
            "Agent {AgentName} ({MachineId}) disconnected",
            agent.Name, agent.MachineId);
    }
    
    public async Task UpdateHeartbeatAsync(string connectionId)
    {
        var agent = await _db.ClientAgents
            .ByConnectionId(connectionId)
            .FirstOrDefaultAsync();
        
        if (agent == null)
        {
            _logger.LogWarning(
                "Heartbeat from unknown connection {ConnectionId}",
                connectionId);
            return;
        }
        
        agent.UpdateHeartbeat();
        await _db.SaveChangesAsync();
    }
    
    public async Task<ClientAgent?> GetAvailableAgentForDeviceAsync(Guid deviceId)
    {
        // Find a connected agent that has an active link to this device
        var agent = await _db.ClientAgents
            .Connected()
            .CanHandleDevice(deviceId)
            .WithDevices()
            .MostRecentFirst()
            .FirstOrDefaultAsync();
        
        return agent;
    }
    
    public async Task<ClientAgent?> GetAgentByConnectionIdAsync(string connectionId)
    {
        return await _db.ClientAgents
            .ByConnectionId(connectionId)
            .FirstOrDefaultAsync();
    }
    
    public async Task<List<ClientAgent>> GetConnectedAgentsAsync()
    {
        return await _db.ClientAgents
            .Connected()
            .WithDevices()
            .MostRecentFirst()
            .ToListAsync();
    }
    
    /// <summary>
    /// Link (or update) devices to an agent
    /// </summary>
    private async Task LinkDevicesToAgentAsync(Guid agentId, List<Guid> deviceIds)
    {
        // Get existing links
        var existingLinks = await _db.AgentDevices
            .Where(ad => ad.AgentId == agentId)
            .ToListAsync();
        
        // Deactivate links not in the new list
        foreach (var link in existingLinks.Where(l => !deviceIds.Contains(l.DeviceId)))
        {
            link.Deactivate();
        }
        
        // Add or activate links for devices in the list
        foreach (var deviceId in deviceIds)
        {
            var existingLink = existingLinks.FirstOrDefault(l => l.DeviceId == deviceId);
            
            if (existingLink != null)
            {
                // Reactivate existing link
                existingLink.Activate();
            }
            else
            {
                // Create new link
                var newLink = new AgentDevice(agentId, deviceId);
                _db.AgentDevices.Add(newLink);
            }
        }
        
        await _db.SaveChangesAsync();
        
        _logger.LogInformation(
            "Linked agent {AgentId} to {DeviceCount} devices",
            agentId, deviceIds.Count);
    }
}