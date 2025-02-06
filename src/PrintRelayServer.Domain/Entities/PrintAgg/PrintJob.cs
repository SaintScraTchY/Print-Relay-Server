using System.Data;
using PrintRelayServer.Domain.Base;
using PrintRelayServer.Domain.Entities.DeviceAgg;
using PrintRelayServer.Domain.Entities.Identity;

namespace PrintRelayServer.Domain.Entities.PrintAgg;

public class PrintJob : Entity<Guid>
{
    public PrintJobStatus Status { get; set; }
    public uint DoneCount { get; set; } = 0;

    public Guid DetailId { get; set; }
    public PrintJobDetail Detail { get; set; }
    
    public Guid RequesterId { get; set; }
    public AppUser Requester { get; set; }
    
    public Guid DeviceId { get; set; }
    public Device Device { get; set; }

    protected PrintJob()
    {
        
    }

    public PrintJob(PrintJobStatus status, Guid requesterId, Guid deviceId)
    {
        Status = status;
        RequesterId = requesterId;
        DeviceId = deviceId;
    }

    public void UpdateStatus(PrintJobStatus newStatus) => Status = newStatus;
}