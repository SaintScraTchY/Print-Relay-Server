using PrintRelayServer.Shared.Contracts.Base;
using PrintRelayServer.Shared.Contracts.PrintJob;

namespace PrintRelayServer.Application.Application.Interfaces;

public interface IPrintApplication
{
    Task<BaseResult<PaginatedResult<GetPrintJob>>> GetPrintJobs(int pageNumber, int pageSize);

    Task<BaseResult<PaginatedResult<GetPrintJob>>> GetPrintJobsByFilter(int pageNumber, int pageSize, GetPrintFilter filter);
}