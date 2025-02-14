using PrintRelayServer.Shared.Contracts.Base;
using PrintRelayServer.Shared.Contracts.PrintJob;

namespace PrintRelayServer.Application.Application.Interfaces;

public interface IPrintApplication
{
    #region PrintJob
    Task<BaseResult<PaginatedResult<GetPrintJob>>> GetPrintJobs(int pageNumber, int pageSize);

    Task<BaseResult<PaginatedResult<GetPrintJob>>> GetPrintJobsByFilter(int pageNumber, int pageSize, GetPrintFilter filter);
    #endregion

    #region
    Task<BaseResult<IList<GetPrintJobEvent>>> GetPrintJobEventsBy(Guid printJobId);
    #endregion
}