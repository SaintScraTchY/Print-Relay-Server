using PrintRelayServer.Domain.Base;

namespace PrintRelayServer.Domain.Entities.PrintAgg;

public class PrintJobDetail: Entity<Guid>
{
    public float? Width { get; set; }
    public float? Height { get; set; }
    public PrintPaperUnit PaperSizeUnit { get; set; }

    public PrintPaper PrintPaper { get; set; }
    
    public PrintPriority Priority { get; set; }
    public uint RequestCount { get; set; }
    public uint CompletedCount { get; set; } = 0;
    public string? Margins { get; set; }
    public PrintQuality Quality { get; set; }

    public string PrinterIdentifier { get; set; }

    protected PrintJobDetail()
    {
        
    }

    public PrintJobDetail( string printerIdentifier, float width = 0f, float height = 0f,
        PrintPaperUnit paperSizeUnit = PrintPaperUnit.Millimeter, PrintPaper printPaper = PrintPaper.A4, 
        PrintPriority priority = PrintPriority.Low, uint requestCount = 1, string margins = "5", PrintQuality quality = PrintQuality.Average)
    {
        Width = width;
        Height = height;
        PaperSizeUnit = paperSizeUnit;
        PrintPaper = printPaper;
        Priority = priority;
        RequestCount = requestCount;
        CompletedCount = 0;
        Margins = margins;
        Quality = quality;
        PrinterIdentifier = printerIdentifier;
    }

    public void SetCustomSize(float width, float height, PrintPaperUnit paperSizeUnit)
    {
        Width = width;
        Height = height;
        PaperSizeUnit = paperSizeUnit;
    }

    public void SetStandardSize(PrintPaper printPaper)
    {
        PrintPaper = printPaper;
    }
}

public enum PrintPaper
{
    Custom = 1,
    A5,
    A4,
    Letter,
    Legal
}

public enum PrintPriority
{
    Low = 1,
    Medium,
    High,
    Asap
}

public enum PrintQuality
{
    Low = 1,
    Average,
    High,
    Excellent
}

public enum PrintPaperUnit
{
    Millimeter = 1,
    Inch,
    Px
}