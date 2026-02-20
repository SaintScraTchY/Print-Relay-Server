using Microsoft.EntityFrameworkCore;
using PrintRelayServer.Domain.Entities.AgentAgg;

namespace PrintRelayServer.Infrastructure.QueryExtensions;

/// <summary>
/// Reusable query extensions for ClientAgent
/// </summary>
public static class ClientAgentQueryExtensions
{
    /// <summary>
    /// Include linked devices
    /// </summary>
    public static IQueryable<ClientAgent> WithDevices(this IQueryable<ClientAgent> query)
    {
        return query.Include(a => a.AgentDevices)
            .ThenInclude(ad => ad.Device);
    }
    
    /// <summary>
    /// Include assigned print jobs
    /// </summary>
    public static IQueryable<ClientAgent> WithJobs(this IQueryable<ClientAgent> query)
    {
        return query.Include(a => a.AssignedJobs);
    }
    
    /// <summary>
    /// Get only connected agents
    /// </summary>
    public static IQueryable<ClientAgent> Connected(this IQueryable<ClientAgent> query)
    {
        return query.Where(a => a.IsConnected);
    }
    
    /// <summary>
    /// Get only disconnected agents
    /// </summary>
    public static IQueryable<ClientAgent> Disconnected(this IQueryable<ClientAgent> query)
    {
        return query.Where(a => !a.IsConnected);
    }
    
    /// <summary>
    /// Filter by SignalR connection ID
    /// </summary>
    public static IQueryable<ClientAgent> ByConnectionId(this IQueryable<ClientAgent> query, string connectionId)
    {
        return query.Where(a => a.SignalRConnectionId == connectionId);
    }
    
    /// <summary>
    /// Filter by machine ID
    /// </summary>
    public static IQueryable<ClientAgent> ByMachineId(this IQueryable<ClientAgent> query, string machineId)
    {
        return query.Where(a => a.MachineId == machineId);
    }
    
    /// <summary>
    /// Filter by owner
    /// </summary>
    public static IQueryable<ClientAgent> OwnedBy(this IQueryable<ClientAgent> query, Guid userId)
    {
        return query.Where(a => a.OwnerId == userId);
    }
    
    /// <summary>
    /// Get agents that can handle a specific device
    /// </summary>
    public static IQueryable<ClientAgent> CanHandleDevice(this IQueryable<ClientAgent> query, Guid deviceId)
    {
        return query.Where(a => a.AgentDevices.Any(ad => 
            ad.DeviceId == deviceId && 
            ad.IsActive));
    }
    
    /// <summary>
    /// Get agents with stale heartbeat (likely disconnected but not marked as such)
    /// </summary>
    public static IQueryable<ClientAgent> WithStaleHeartbeat(
        this IQueryable<ClientAgent> query, 
        int thresholdSeconds = 90)
    {
        var threshold = DateTime.UtcNow.AddSeconds(-thresholdSeconds);
        return query.Where(a => 
            a.IsConnected && 
            a.LastHeartbeat.HasValue && 
            a.LastHeartbeat.Value < threshold);
    }
    
    /// <summary>
    /// Order by last heartbeat (most recent first)
    /// </summary>
    public static IQueryable<ClientAgent> MostRecentFirst(this IQueryable<ClientAgent> query)
    {
        return query.OrderByDescending(a => a.LastHeartbeat);
    }
}

// EXAMPLE USAGE:
// var availableAgents = await _db.ClientAgents
//     .Connected()
//     .CanHandleDevice(deviceId)
//     .WithDevices()
//     .MostRecentFirst()
//     .FirstOrDefaultAsync();