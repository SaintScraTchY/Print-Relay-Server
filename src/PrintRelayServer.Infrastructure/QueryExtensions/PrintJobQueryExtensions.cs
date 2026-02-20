using Microsoft.EntityFrameworkCore;
using PrintRelayServer.Domain.Entities.PrintAgg;

namespace PrintRelayServer.Infrastructure.QueryExtensions;

/// <summary>
/// Reusable query extensions for PrintJob.
/// These replace the need for IRepository by providing composable query methods.
/// </summary>
public static class PrintJobQueryExtensions
{
    /// <summary>
    /// Include device information
    /// </summary>
    public static IQueryable<PrintJob> WithDevice(this IQueryable<PrintJob> query)
    {
        return query.Include(j => j.Device);
    }
    
    /// <summary>
    /// Include file information
    /// </summary>
    public static IQueryable<PrintJob> WithFile(this IQueryable<PrintJob> query)
    {
        return query.Include(j => j.File);
    }
    
    /// <summary>
    /// Include job details (paper size, quality, etc.)
    /// </summary>
    public static IQueryable<PrintJob> WithDetails(this IQueryable<PrintJob> query)
    {
        return query.Include(j => j.Detail);
    }
    
    /// <summary>
    /// Include assigned agent information
    /// </summary>
    public static IQueryable<PrintJob> WithAgent(this IQueryable<PrintJob> query)
    {
        return query.Include(j => j.AssignedAgent);
    }
    
    /// <summary>
    /// Include full related data (device, file, details, agent)
    /// </summary>
    public static IQueryable<PrintJob> WithFullDetails(this IQueryable<PrintJob> query)
    {
        return query
            .Include(j => j.Device)
            .Include(j => j.File)
            .Include(j => j.Detail)
            .Include(j => j.AssignedAgent);
    }
    
    /// <summary>
    /// Filter by status
    /// </summary>
    public static IQueryable<PrintJob> WithStatus(this IQueryable<PrintJob> query, PrintJobStatus status)
    {
        return query.Where(j => j.Status == status);
    }
    
    /// <summary>
    /// Get pending jobs only
    /// </summary>
    public static IQueryable<PrintJob> Pending(this IQueryable<PrintJob> query)
    {
        return query.Where(j => j.Status == PrintJobStatus.Pending);
    }
    
    /// <summary>
    /// Get active (non-terminal) jobs
    /// </summary>
    public static IQueryable<PrintJob> Active(this IQueryable<PrintJob> query)
    {
        return query.Where(j => 
            j.Status != PrintJobStatus.Completed && 
            j.Status != PrintJobStatus.Failed && 
            j.Status != PrintJobStatus.Cancelled);
    }
    
    /// <summary>
    /// Get completed jobs only
    /// </summary>
    public static IQueryable<PrintJob> Completed(this IQueryable<PrintJob> query)
    {
        return query.Where(j => j.Status == PrintJobStatus.Completed);
    }
    
    /// <summary>
    /// Get failed jobs only
    /// </summary>
    public static IQueryable<PrintJob> Failed(this IQueryable<PrintJob> query)
    {
        return query.Where(j => j.Status == PrintJobStatus.Failed);
    }
    
    /// <summary>
    /// Filter by device
    /// </summary>
    public static IQueryable<PrintJob> ForDevice(this IQueryable<PrintJob> query, Guid deviceId)
    {
        return query.Where(j => j.DeviceId == deviceId);
    }
    
    /// <summary>
    /// Filter by user
    /// </summary>
    public static IQueryable<PrintJob> CreatedBy(this IQueryable<PrintJob> query, Guid userId)
    {
        return query.Where(j => j.CreatedById == userId);
    }
    
    /// <summary>
    /// Filter by assigned agent
    /// </summary>
    public static IQueryable<PrintJob> AssignedTo(this IQueryable<PrintJob> query, Guid agentId)
    {
        return query.Where(j => j.AssignedAgentId == agentId);
    }
    
    /// <summary>
    /// Get jobs created within specified days
    /// </summary>
    public static IQueryable<PrintJob> Recent(this IQueryable<PrintJob> query, int days = 7)
    {
        var since = DateTime.UtcNow.AddDays(-days);
        return query.Where(j => j.CreatedAt >= since);
    }
    
    /// <summary>
    /// Get jobs created within date range
    /// </summary>
    public static IQueryable<PrintJob> CreatedBetween(
        this IQueryable<PrintJob> query, 
        DateTime startDate, 
        DateTime endDate)
    {
        return query.Where(j => j.CreatedAt >= startDate && j.CreatedAt <= endDate);
    }
    
    /// <summary>
    /// Order by creation time (newest first)
    /// </summary>
    public static IQueryable<PrintJob> NewestFirst(this IQueryable<PrintJob> query)
    {
        return query.OrderByDescending(j => j.CreatedAt);
    }
    
    /// <summary>
    /// Order by creation time (oldest first)
    /// </summary>
    public static IQueryable<PrintJob> OldestFirst(this IQueryable<PrintJob> query)
    {
        return query.OrderBy(j => j.CreatedAt);
    }
}

// EXAMPLE USAGE:
// var recentFailedJobs = await _db.PrintJobs
//     .Failed()
//     .ForDevice(deviceId)
//     .Recent(30)
//     .WithDevice()
//     .WithFile()
//     .NewestFirst()
//     .Take(10)
//     .ToListAsync();