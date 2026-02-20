using Microsoft.AspNetCore.SignalR;
using PrintRelayServer.Application.Application.Interfaces;
using PrintRelayServer.Domain.Entities.PrintAgg;

namespace PrintRelayServer.API.Hubs;

/// <summary>
/// SignalR hub for real-time communication with print agents.
/// Handles agent connection, job assignment, and status updates.
/// </summary>
public class PrintRelayHub : Hub
{
    private readonly IAgentRegistryService _agentRegistry;
    private readonly IPrintJobService _printJobService;
    private readonly ILogger<PrintRelayHub> _logger;
    
    public PrintRelayHub(
        IAgentRegistryService agentRegistry,
        IPrintJobService printJobService,
        ILogger<PrintRelayHub> logger)
    {
        _agentRegistry = agentRegistry;
        _printJobService = printJobService;
        _logger = logger;
    }
    
    // ===========================
    // CLIENT → SERVER METHODS
    // ===========================
    
    /// <summary>
    /// Called when an agent connects and registers itself.
    /// Agent provides list of devices it can access.
    /// </summary>
    /// <param name="agentName">Friendly name (e.g., "Office PC - John's Desk")</param>
    /// <param name="machineId">Unique machine identifier</param>
    /// <param name="deviceIds">List of device IDs this agent can print to</param>
    public async Task RegisterAgent(string agentName, string machineId, List<Guid> deviceIds)
    {
        try
        {
            var agent = await _agentRegistry.RegisterAgentAsync(
                agentName,
                machineId,
                Context.ConnectionId,
                deviceIds);
            
            _logger.LogInformation(
                "Agent registered: {AgentName} (Connection: {ConnectionId}, Devices: {DeviceCount})",
                agentName, Context.ConnectionId, deviceIds.Count);
            
            // Send confirmation back to agent
            await Clients.Caller.SendAsync("RegistrationConfirmed", agent.Id, agent.Name);
            
            // Try to assign any pending jobs for these devices
            await TryAssignPendingJobsAsync(deviceIds);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to register agent {AgentName}", agentName);
            await Clients.Caller.SendAsync("RegistrationFailed", ex.Message);
        }
    }
    
    /// <summary>
    /// Heartbeat from agent to show it's still alive
    /// </summary>
    public async Task Heartbeat()
    {
        try
        {
            await _agentRegistry.UpdateHeartbeatAsync(Context.ConnectionId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Heartbeat failed for connection {ConnectionId}", Context.ConnectionId);
        }
    }
    
    /// <summary>
    /// Agent reports status update for a job
    /// </summary>
    /// <param name="jobId">Print job ID</param>
    /// <param name="status">New status</param>
    /// <param name="errorMessage">Error message if status is Failed</param>
    public async Task UpdateJobStatus(Guid jobId, PrintJobStatus status, string? errorMessage = null)
    {
        try
        {
            await _printJobService.UpdateJobStatusAsync(jobId, status, errorMessage);
            
            _logger.LogInformation(
                "Job {JobId} status updated to {Status} by connection {ConnectionId}",
                jobId, status, Context.ConnectionId);
            
            // Notify all connected web clients about the status change
            await Clients.All.SendAsync("JobStatusChanged", jobId, status, errorMessage);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, 
                "Failed to update job {JobId} status to {Status}", 
                jobId, status);
            
            await Clients.Caller.SendAsync("StatusUpdateFailed", jobId, ex.Message);
        }
    }
    
    // ===========================
    // SERVER → CLIENT METHODS
    // (These are called from services, not directly from agents)
    // ===========================
    
    /// <summary>
    /// Send a print job to a specific agent.
    /// Called by PrintJobService after job assignment.
    /// </summary>
    public async Task SendPrintJobToAgent(string connectionId, Guid jobId, Guid fileId, Guid deviceId)
    {
        await Clients.Client(connectionId).SendAsync(
            "PrintJobAssigned", 
            jobId, 
            fileId, 
            deviceId);
        
        _logger.LogInformation(
            "Sent print job {JobId} to agent connection {ConnectionId}",
            jobId, connectionId);
    }
    
    /// <summary>
    /// Request agent to cancel a job
    /// </summary>
    public async Task SendCancelJobToAgent(string connectionId, Guid jobId)
    {
        await Clients.Client(connectionId).SendAsync("CancelJob", jobId);
        
        _logger.LogInformation(
            "Sent cancel request for job {JobId} to agent connection {ConnectionId}",
            jobId, connectionId);
    }
    
    // ===========================
    // CONNECTION LIFECYCLE
    // ===========================
    
    public override async Task OnConnectedAsync()
    {
        _logger.LogInformation(
            "Client connected: {ConnectionId} from {IPAddress}",
            Context.ConnectionId,
            Context.GetHttpContext()?.Connection.RemoteIpAddress);
        
        await base.OnConnectedAsync();
    }
    
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await _agentRegistry.UnregisterAgentAsync(Context.ConnectionId);
        
        if (exception != null)
        {
            _logger.LogWarning(exception,
                "Client disconnected with error: {ConnectionId}",
                Context.ConnectionId);
        }
        else
        {
            _logger.LogInformation(
                "Client disconnected: {ConnectionId}",
                Context.ConnectionId);
        }
        
        await base.OnDisconnectedAsync(exception);
    }
    
    // ===========================
    // HELPER METHODS
    // ===========================
    
    /// <summary>
    /// Try to assign any pending jobs for the newly connected devices
    /// </summary>
    private async Task TryAssignPendingJobsAsync(List<Guid> deviceIds)
    {
        foreach (var deviceId in deviceIds)
        {
            var pendingJobs = await _printJobService.GetPendingJobsForDeviceAsync(deviceId);
            
            foreach (var job in pendingJobs)
            {
                await TryAssignJobAsync(job.Id, deviceId);
            }
        }
    }
    
    /// <summary>
    /// Try to assign a specific job to an available agent
    /// </summary>
    private async Task TryAssignJobAsync(Guid jobId, Guid deviceId)
    {
        try
        {
            // Find available agent for this device
            var agent = await _agentRegistry.GetAvailableAgentForDeviceAsync(deviceId);
            
            if (agent == null)
            {
                _logger.LogWarning(
                    "No available agent found for device {DeviceId}, job {JobId} remains pending",
                    deviceId, jobId);
                return;
            }
            
            if (string.IsNullOrEmpty(agent.SignalRConnectionId))
            {
                _logger.LogWarning(
                    "Agent {AgentId} has no connection ID",
                    agent.Id);
                return;
            }
            
            // Assign job to agent
            await _printJobService.AssignJobToAgentAsync(jobId, agent.Id);
            
            // Get job details to send file ID
            var job = await _printJobService.GetJobByIdAsync(jobId);
            if (job == null) return;
            
            // Send job to agent via SignalR
            await SendPrintJobToAgent(
                agent.SignalRConnectionId, 
                jobId, 
                job.FileId, 
                deviceId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, 
                "Failed to assign job {JobId} for device {DeviceId}", 
                jobId, deviceId);
        }
    }
}