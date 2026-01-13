namespace PrintRelayServer.Domain.Entities.PrintAgg;

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