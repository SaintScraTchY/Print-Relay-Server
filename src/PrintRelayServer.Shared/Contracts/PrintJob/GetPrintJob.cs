namespace PrintRelayServer.Shared.Contracts.PrintJob;

public class GetPrintJob
{
    public string Status { get; set; }
    public uint DoneCount { get; set; } = 0;

    public Guid DetailId { get; set; }
    
    public Guid RequesterId { get; set; }
    public string RequesterFullName { get; set; }
    
    public Guid DeviceId { get; set; }
    public string DeviceName { get; set; }

    public GetPrintJobDetail PrintDetail { get; set; }
}