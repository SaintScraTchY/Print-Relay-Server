using PrintRelayServer.Domain.Base;

namespace PrintRelayServer.Domain.Entities.PrintAgg;

public class PrintJobEvent : Entity<Guid>
{
    public PrintJobStatus Status { get; set; }
    public ushort QueuePosition { get; set; }

    public string? Details { get; set; }

    protected PrintJobEvent()
    {
        
    }

    #region Navigations

    public Guid PrintJobId { get; set; }
    public PrintJob PrintJob { get; set; }

    #endregion
}

public enum PrintJobStatus
{
    Sent = 1,
    Approved,
    InQueue,
    Paused,
    Cancelled,
    Printing,
    Failed,
    Printed
}