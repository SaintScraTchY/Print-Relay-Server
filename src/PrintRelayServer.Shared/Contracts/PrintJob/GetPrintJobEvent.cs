namespace PrintRelayServer.Shared.Contracts.PrintJob;

public class GetPrintJobEvent
{
    public string Status { get; set; }
    public ushort QueuePosition { get; set; }
    public string? Details { get; set; }
}