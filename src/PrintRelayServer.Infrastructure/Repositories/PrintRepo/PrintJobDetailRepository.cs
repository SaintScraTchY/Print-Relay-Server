using PrintRelayServer.Domain.Entities.PrintAgg;
using PrintRelayServer.Domain.IRepositories.IPrintRepo;
using PrintRelayServer.Infrastructure.Contexts;

namespace PrintRelayServer.Infrastructure.Repositories.PrintRepo;

public class PrintJobDetailRepository : Repository<PrintJobDetail,Guid>,IPrintJobDetailRepository
{
    private readonly PrintRelayContext _context;
    public PrintJobDetailRepository(PrintRelayContext context) : base(context)
    {
        _context = context;
    }
}