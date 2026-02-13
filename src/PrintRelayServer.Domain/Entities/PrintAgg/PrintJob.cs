using PrintRelayServer.Domain.Base;
using PrintRelayServer.Domain.Entities.AgentAgg;
using PrintRelayServer.Domain.Entities.DeviceAgg;
using PrintRelayServer.Domain.Entities.FileAgg;

namespace PrintRelayServer.Domain.Entities.PrintAgg;

/// <summary>
/// Represents a print job request from a user.
/// Tracks the complete lifecycle from creation to completion/failure.
/// </summary>
public class PrintJob : AuditableEntity
{
    // Core references
    public Guid DeviceId { get; private set; }
    public Device Device { get; set; }
    
    public Guid FileId { get; private set; }
    public ManagedFile File { get; set; }
    
    public Guid DetailId { get; private set; }
    public PrintJobDetail Detail { get; set; }
    
    // Assignment tracking
    public Guid? AssignedAgentId { get; private set; }
    public ClientAgent? AssignedAgent { get; set; }
    
    // Lifecycle timestamps
    public PrintJobStatus Status { get; private set; }
    public DateTime? AssignedAt { get; private set; }
    public DateTime? StartedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    
    // Error handling
    public string? ErrorMessage { get; private set; }
    public int RetryCount { get; private set; }
    
    // EF Constructor
    protected PrintJob(Guid userGuid) { }
    
    /// <summary>
    /// Create a new print job
    /// </summary>
    public PrintJob(Guid userId, Guid deviceId, Guid fileId, Guid detailId)
    {
        DeviceId = deviceId;
        FileId = fileId;
        DetailId = detailId;
        Status = PrintJobStatus.Pending;
        RetryCount = 0;
    }
    
    /// <summary>
    /// Assign this job to an agent
    /// </summary>
    public void AssignToAgent(Guid agentId)
    {
        if (Status != PrintJobStatus.Pending)
            throw new InvalidOperationException($"Cannot assign job in status {Status}");
        
        AssignedAgentId = agentId;
        AssignedAt = DateTime.UtcNow;
        Status = PrintJobStatus.Assigned;
    }
    
    /// <summary>
    /// Mark job as downloading by agent
    /// </summary>
    public void MarkAsDownloading()
    {
        if (Status != PrintJobStatus.Assigned)
            throw new InvalidOperationException($"Cannot start download from status {Status}");
        
        Status = PrintJobStatus.Downloading;
    }
    
    /// <summary>
    /// Mark job as queued in printer
    /// </summary>
    public void MarkAsQueued()
    {
        if (Status != PrintJobStatus.Downloading)
            throw new InvalidOperationException($"Cannot queue from status {Status}");
        
        Status = PrintJobStatus.InQueue;
    }
    
    /// <summary>
    /// Start printing
    /// </summary>
    public void StartPrinting()
    {
        if (Status != PrintJobStatus.InQueue && Status != PrintJobStatus.Paused)
            throw new InvalidOperationException($"Cannot start printing from status {Status}");
        
        StartedAt = DateTime.UtcNow;
        Status = PrintJobStatus.Printing;
    }
    
    /// <summary>
    /// Pause printing
    /// </summary>
    public void Pause()
    {
        if (Status != PrintJobStatus.Printing && Status != PrintJobStatus.InQueue)
            throw new InvalidOperationException($"Cannot pause from status {Status}");
        
        Status = PrintJobStatus.Paused;
    }
    
    /// <summary>
    /// Mark job as completed successfully
    /// </summary>
    public void Complete()
    {
        if (Status != PrintJobStatus.Printing)
            throw new InvalidOperationException($"Cannot complete from status {Status}");
        
        CompletedAt = DateTime.UtcNow;
        Status = PrintJobStatus.Completed;
    }
    
    /// <summary>
    /// Mark job as failed with error message
    /// </summary>
    public void Fail(string errorMessage)
    {
        ErrorMessage = errorMessage;
        Status = PrintJobStatus.Failed;
        CompletedAt = DateTime.UtcNow;
    }
    
    /// <summary>
    /// Cancel the job (by user or system)
    /// </summary>
    public void Cancel(Guid userId)
    {
        if (Status == PrintJobStatus.Completed || Status == PrintJobStatus.Failed)
            throw new InvalidOperationException($"Cannot cancel job in status {Status}");
        
        Status = PrintJobStatus.Cancelled;
        CompletedAt = DateTime.UtcNow;
    }
    
    /// <summary>
    /// Retry failed job (resets to Pending)
    /// </summary>
    public void Retry()
    {
        if (Status != PrintJobStatus.Failed)
            throw new InvalidOperationException("Can only retry failed jobs");
        
        RetryCount++;
        Status = PrintJobStatus.Pending;
        AssignedAgentId = null;
        AssignedAt = null;
        StartedAt = null;
        CompletedAt = null;
        ErrorMessage = null;
    }
    
    /// <summary>
    /// Check if job is in terminal state
    /// </summary>
    public bool IsTerminal => 
        Status == PrintJobStatus.Completed || 
        Status == PrintJobStatus.Failed || 
        Status == PrintJobStatus.Cancelled;
}