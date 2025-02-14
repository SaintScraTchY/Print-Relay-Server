namespace PrintRelayServer.Shared.Contracts.PrintJob;

public class GetPrintJobDetail
{
    public float? Width { get; set; }
    public float? Height { get; set; }
    public string PaperSizeUnit { get; set; }

    public string PrintPaper { get; set; }
    
    public string Priority { get; set; }
    public uint Copies { get; set; }
    public string? Margins { get; set; }
    public string Quality { get; set; }

    public string PrinterIdentifier { get; set; }
}