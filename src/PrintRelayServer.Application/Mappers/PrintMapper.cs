using PrintRelayServer.Domain.Entities.PrintAgg;
using PrintRelayServer.Shared.Contracts.PrintJob;
using Riok.Mapperly.Abstractions;

namespace PrintRelayServer.Application.Mappers;

[Mapper]
public static partial class PrintMapper
{
    public static partial GetPrintJob MapToPrintJob(PrintJob source);
    public static partial IList<GetPrintJob> MapToPrintJobs(IList<PrintJob> source);
}