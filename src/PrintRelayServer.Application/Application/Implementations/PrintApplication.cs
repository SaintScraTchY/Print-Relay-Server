using PrintRelayServer.Application.Application.Interfaces;
using PrintRelayServer.Application.Mappers;
using PrintRelayServer.Domain.IRepositories.IPrintRepo;
using PrintRelayServer.Shared.Contracts.Base;
using PrintRelayServer.Shared.Contracts.PrintJob;

namespace PrintRelayServer.Application.Application.Implementations;

public class PrintApplication : IPrintApplication
{
    private readonly IPrintJobRepository _printJobRepository;
    private readonly IPrintJobEventRepository _printJobEventRepository;
    private readonly IPrintJobDetailRepository _printJobDetailRepository;

    public PrintApplication(IPrintJobRepository printJobRepository, IPrintJobEventRepository printJobEventRepository, IPrintJobDetailRepository printJobDetailRepository)
    {
        _printJobRepository = printJobRepository;
        _printJobEventRepository = printJobEventRepository;
        _printJobDetailRepository = printJobDetailRepository;
    }

    public async Task<BaseResult<PaginatedResult<GetPrintJob>>> GetPrintJobs(int pageNumber, int pageSize)
    {
        var printJobs = await _printJobRepository
            .GetPaginatedAsync(pageNumber,pageSize,orderBy: x => x.OrderByDescending(d => d.Id), includes: "Detail");

        var result = new PaginatedResult<GetPrintJob>
        {
            Results = PrintMapper.MapToPrintJobs(printJobs.Results),
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = printJobs.TotalCount,
            TotalPages = printJobs.TotalPages
        };

        return ReturnResult<PaginatedResult<GetPrintJob>>.Success(result);
    }

    public async Task<BaseResult<PaginatedResult<GetPrintJob>>> GetPrintJobsByFilter(int pageNumber,int pageSize,GetPrintFilter filter)
    {
        var printJobs = await _printJobRepository.GetPaginatedAsync(pageNumber,pageSize,
            filter: x=> 
                (filter.Requesters == null || filter.Requesters.Contains(x.CreatedById))
            && (filter.Devices == null || filter.Devices.Contains(x.DeviceId))
                && (filter.Statuses == null || filter.Statuses.Contains((short)x.Status))
                && (filter.DateFrom == null || filter.DateFrom >= DateTime.Today)
                && (filter.DateTo == null || filter.DateTo <= DateTime.Today),
            orderBy: x => x.OrderByDescending(d => d.Id), includes: "Detail");

        var result = new PaginatedResult<GetPrintJob>
        {
            Results = PrintMapper.MapToPrintJobs(printJobs.Results),
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = printJobs.TotalCount,
            TotalPages = printJobs.TotalPages
        };
        
        return ReturnResult<PaginatedResult<GetPrintJob>>.Success(result);
    }

    public async Task<BaseResult<IList<GetPrintJobEvent>>> GetPrintJobEventsBy(Guid printJobId)
    {
        var printJobEvents = await _printJobEventRepository
            .GetAllAsync(filter:x=>x.PrintJobId == printJobId,
                orderBy: x => x.OrderByDescending(d => d.Id));

        return ReturnResult<IList<GetPrintJobEvent>>.Success(PrintMapper.MapToPrintJobEvents(printJobEvents));
    }
}