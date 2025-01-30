using PrintRelayServer.Domain.Base;
using PrintRelayServer.Domain.Entities.DeviceAgg;
using PrintRelayServer.Domain.Entities.Identity;

namespace PrintRelayServer.Domain.Entities.PrintAgg;

public class PrintJob : Entity<Guid>
{
    public PrintJobStatus Status { get; set; }

    public string? Details { get; set; }
    
    public Guid RequesterId { get; set; }
    public AppUser Requester { get; set; }
    
    public Guid DeviceId { get; set; }
    public Device Device { get; set; }
}