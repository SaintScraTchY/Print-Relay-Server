using PrintRelayServer.Application.Application.Interfaces;
using PrintRelayServer.Domain.Entities.PrintAgg;
using PrintRelayServer.Infrastructure.Contexts;
using PrintRelayServer.Infrastructure.QueryExtensions;

namespace PrintRelayServer.Application.Application.Implementations;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
/// <summary>
/// Core service for managing print job lifecycle.
/// Handles job creation, assignment, and status updates.
/// </summary>
public class PrintJobService : IPrintJobService
{
    private readonly PrintRelayContext _db;
    private readonly ILogger<PrintJobService> _logger;
    
    public PrintJobService(
        PrintRelayContext db,
        ILogger<PrintJobService> logger)
    {
        _db = db;
        _logger = logger;
    }
    
    public async Task<Guid> CreatePrintJobAsync(
        Guid userId, 
        Guid deviceId, 
        Guid fileId, 
        PrintJobDetail detail)
    {
        // Validate device exists
        var deviceExists = await _db.Devices.AnyAsync(d => d.Id == deviceId);
        if (!deviceExists)
            throw new InvalidOperationException($"Device {deviceId} not found");
        
        // Validate file exists
        var fileExists = await _db.ManagedFiles.AnyAsync(f => f.Id == fileId);
        if (!fileExists)
            throw new InvalidOperationException($"File {fileId} not found");
        
        // Save print detail first
        _db.PrintJobDetails.Add(detail);
        await _db.SaveChangesAsync();
        
        // Create print job
        var job = new PrintJob(userId, deviceId, fileId, detail.Id);
        
        _db.PrintJobs.Add(job);
        await _db.SaveChangesAsync();
        
        _logger.LogInformation(
            "Created print job {JobId} for device {DeviceId} by user {UserId}",
            job.Id, deviceId, userId);
        
        return job.Id;
    }
    
    public async Task AssignJobToAgentAsync(Guid jobId, Guid agentId)
    {
        var job = await _db.PrintJobs
            .FirstOrDefaultAsync(j => j.Id == jobId);
        
        if (job == null)
            throw new InvalidOperationException($"Job {jobId} not found");
        
        // Verify agent exists and is connected
        var agent = await _db.ClientAgents
            .FirstOrDefaultAsync(a => a.Id == agentId && a.IsConnected);
        
        if (agent == null)
            throw new InvalidOperationException($"Agent {agentId} not found or not connected");
        
        job.AssignToAgent(agentId);
        await _db.SaveChangesAsync();
        
        _logger.LogInformation(
            "Assigned job {JobId} to agent {AgentId}",
            jobId, agentId);
    }
    
    public async Task UpdateJobStatusAsync(
        Guid jobId, 
        PrintJobStatus status, 
        string? errorMessage = null)
    {
        var job = await _db.PrintJobs
            .FirstOrDefaultAsync(j => j.Id == jobId);
        
        if (job == null)
            throw new InvalidOperationException($"Job {jobId} not found");
        
        // Update status based on state machine
        switch (status)
        {
            case PrintJobStatus.Downloading:
                job.MarkAsDownloading();
                break;
            
            case PrintJobStatus.InQueue:
                job.MarkAsQueued();
                break;
            
            case PrintJobStatus.Printing:
                job.StartPrinting();
                break;
            
            case PrintJobStatus.Paused:
                job.Pause();
                break;
            
            case PrintJobStatus.Completed:
                job.Complete();
                break;
            
            case PrintJobStatus.Failed:
                job.Fail(errorMessage ?? "Unknown error");
                break;
            
            case PrintJobStatus.Cancelled:
                // Cancellation should go through CancelJobAsync
                throw new InvalidOperationException("Use CancelJobAsync to cancel jobs");
            
            default:
                throw new ArgumentException($"Invalid status transition to {status}");
        }
        
        await _db.SaveChangesAsync();
        
        _logger.LogInformation(
            "Updated job {JobId} status to {Status}",
            jobId, status);
        
        if (status == PrintJobStatus.Failed)
        {
            _logger.LogWarning(
                "Job {JobId} failed: {Error}",
                jobId, errorMessage);
        }
    }
    
    public async Task<List<PrintJob>> GetPendingJobsForDeviceAsync(Guid deviceId)
    {
        return await _db.PrintJobs
            .Pending()
            .ForDevice(deviceId)
            .WithFile()
            .WithDetails()
            .OldestFirst()
            .ToListAsync();
    }
    
    public async Task<PrintJob?> GetJobByIdAsync(Guid jobId)
    {
        return await _db.PrintJobs
            .WithFullDetails()
            .FirstOrDefaultAsync(j => j.Id == jobId);
    }
    
    public async Task CancelJobAsync(Guid jobId, Guid userId)
    {
        var job = await _db.PrintJobs
            .FirstOrDefaultAsync(j => j.Id == jobId);
        
        if (job == null)
            throw new InvalidOperationException($"Job {jobId} not found");
        
        job.Cancel(userId);
        await _db.SaveChangesAsync();
        
        _logger.LogInformation(
            "Job {JobId} cancelled by user {UserId}",
            jobId, userId);
    }
}