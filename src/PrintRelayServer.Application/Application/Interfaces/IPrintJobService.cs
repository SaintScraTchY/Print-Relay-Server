using PrintRelayServer.Domain.Entities.PrintAgg;

namespace PrintRelayServer.Application.Application.Interfaces;

/// <summary>
/// Service for managing print job lifecycle
/// </summary>
public interface IPrintJobService
{
    /// <summary>
    /// Create a new print job
    /// </summary>
    /// <param name="userId">User creating the job</param>
    /// <param name="deviceId">Target device/printer</param>
    /// <param name="fileId">File to print</param>
    /// <param name="detail">Print settings</param>
    /// <returns>Created job ID</returns>
    Task<Guid> CreatePrintJobAsync(
        Guid userId, 
        Guid deviceId, 
        Guid fileId, 
        PrintJobDetail detail);
    
    /// <summary>
    /// Assign a pending job to an agent
    /// </summary>
    Task AssignJobToAgentAsync(Guid jobId, Guid agentId);
    
    /// <summary>
    /// Update job status (called by agent)
    /// </summary>
    Task UpdateJobStatusAsync(
        Guid jobId, 
        PrintJobStatus status, 
        string? errorMessage = null);
    
    /// <summary>
    /// Get pending jobs for a specific device
    /// </summary>
    Task<List<PrintJob>> GetPendingJobsForDeviceAsync(Guid deviceId);
    
    /// <summary>
    /// Get job by ID with all details
    /// </summary>
    Task<PrintJob?> GetJobByIdAsync(Guid jobId);
    
    /// <summary>
    /// Cancel a job (by user or system)
    /// </summary>
    Task CancelJobAsync(Guid jobId, Guid userId);
}