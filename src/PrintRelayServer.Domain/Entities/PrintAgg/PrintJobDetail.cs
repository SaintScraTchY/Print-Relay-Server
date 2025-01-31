using PrintRelayServer.Domain.Base;

namespace PrintRelayServer.Domain.Entities.PrintAgg;

public class PrintJobDetail: Entity<Guid>
{
    public bool IsCustomPaper { get; set; }
    
    public float Width { get; set; }
    public float Height { get; set; }
    public PrintPaperUnit PaperSizeUnit { get; set; }

    public PrintPaper PrintPaper { get; set; }
    
    public PrintPriority Priority { get; set; }
    public uint Copies { get; set; } = 1;
    public string Margins { get; set; }
    public PrintQuality Quality { get; set; }

    public string PrinterIdentifier { get; set; }

    public PrintJobDetail( string printerIdentifier,bool isCustomPaper = false, float width = 0f, float height = 0f,
        PrintPaperUnit paperSizeUnit = PrintPaperUnit.Millimeter, PrintPaper printPaper = PrintPaper.A4, 
        PrintPriority priority = PrintPriority.Low, uint copies = 1, string margins = "5", PrintQuality quality = PrintQuality.Average)
    {
        IsCustomPaper = isCustomPaper;
        Width = width;
        Height = height;
        PaperSizeUnit = paperSizeUnit;
        PrintPaper = printPaper;
        Priority = priority;
        Copies = copies;
        Margins = margins;
        Quality = quality;
        PrinterIdentifier = printerIdentifier;
    }

    public void SetCustomSize(float width, float height, PrintPaperUnit paperSizeUnit)
    {
        IsCustomPaper = true;
        Width = width;
        Height = height;
        PaperSizeUnit = paperSizeUnit;
    }

    public void SetStandardSize(PrintPaper printPaper)
    {
        IsCustomPaper = false;
        PrintPaper = printPaper;
    }
}

public enum PrintPaper
{
    A5 = 1,
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