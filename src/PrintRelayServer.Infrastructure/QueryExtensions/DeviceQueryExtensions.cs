using Microsoft.EntityFrameworkCore;
using PrintRelayServer.Domain.Entities.DeviceAgg;

namespace PrintRelayServer.Infrastructure.QueryExtensions;

/// <summary>
/// Reusable query extensions for Device
/// </summary>
public static class DeviceQueryExtensions
{
    /// <summary>
    /// Include device type information
    /// </summary>
    public static IQueryable<Device> WithDeviceType(this IQueryable<Device> query)
    {
        return query.Include(d => d.DeviceType);
    }
    
    /// <summary>
    /// Include owner information
    /// </summary>
    public static IQueryable<Device> WithOwner(this IQueryable<Device> query)
    {
        return query.Include(d => d.Owner);
    }
    
    /// <summary>
    /// Include linked agents
    /// </summary>
    public static IQueryable<Device> WithAgents(this IQueryable<Device> query)
    {
        return query.Include(d => d.AgentDevices)
            .ThenInclude(ad => ad.Agent);
    }
    
    /// <summary>
    /// Filter by owner
    /// </summary>
    public static IQueryable<Device> OwnedBy(this IQueryable<Device> query, Guid userId)
    {
        return query.Where(d => d.OwnerId == userId);
    }
    
    /// <summary>
    /// Filter by device type
    /// </summary>
    public static IQueryable<Device> OfType(this IQueryable<Device> query, Guid deviceTypeId)
    {
        return query.Where(d => d.DeviceTypeId == deviceTypeId);
    }
    
    /// <summary>
    /// Get devices that have at least one connected agent
    /// </summary>
    public static IQueryable<Device> WithConnectedAgent(this IQueryable<Device> query)
    {
        return query.Where(d => d.AgentDevices.Any(ad => 
            ad.IsActive && 
            ad.Agent.IsConnected));
    }
    
    /// <summary>
    /// Get devices with no connected agents
    /// </summary>
    public static IQueryable<Device> WithoutConnectedAgent(this IQueryable<Device> query)
    {
        return query.Where(d => !d.AgentDevices.Any(ad => 
            ad.IsActive && 
            ad.Agent.IsConnected));
    }
    
    /// <summary>
    /// Search by name (case-insensitive)
    /// </summary>
    public static IQueryable<Device> SearchByName(this IQueryable<Device> query, string searchTerm)
    {
        return query.Where(d => EF.Functions.ILike(d.Name, $"%{searchTerm}%"));
    }
}

// EXAMPLE USAGE:
// var onlineDevices = await _db.Devices
//     .OwnedBy(userId)
//     .WithConnectedAgent()
//     .WithDeviceType()
//     .WithAgents()
//     .ToListAsync();