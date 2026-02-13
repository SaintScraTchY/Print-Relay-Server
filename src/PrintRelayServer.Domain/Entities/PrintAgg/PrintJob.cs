using System.Data;
using PrintRelayServer.Domain.Base;
using PrintRelayServer.Domain.Entities.DeviceAgg;
using PrintRelayServer.Domain.Entities.FileAgg;
using PrintRelayServer.Domain.Entities.Identity;

namespace PrintRelayServer.Domain.Entities.PrintAgg;

public class PrintJob : AuditableEntity
{
    protected PrintJob(DateTime? startedAt, DateTime? completedAt, Guid detailId, Guid fileId)
    {
        StartedAt = startedAt;
        CompletedAt = completedAt;
        DetailId = detailId;
        FileId = fileId;
    }

    public PrintJobStatus Status { get; set; }
    
    public DateTime? AssignedAt { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    
    public string? ErrorMessage { get; set; }
    public int RetryCount { get; set; }
    
    // Navigation
    public Guid DetailId { get; set; }
    public PrintJobDetail Detail { get; set; }
    
    public Guid FileId { get; set; }
    public ManagedFile File { get; set; }
    
    public Guid? AssignedAgentId { get; set; }
    public ClientAgent? AssignedAgent { get; set; }
    
    public Guid DeviceId { get; set; }
    public Device Device { get; set; }


    public void UpdateStatus(PrintJobStatus newStatus) => Status = newStatus;
}