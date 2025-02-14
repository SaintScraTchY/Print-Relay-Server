namespace PrintRelayServer.Shared.Contracts.PrintJob;

public class GetPrintFilter
{
    public IEnumerable<Guid>? Requesters { get; set; }
    public IEnumerable<Guid>? Devices { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public IEnumerable<short>? Statuses { get; set; }
}