using PrintRelayServer.Domain.Entities.PrintAgg;
using PrintRelayServer.Domain.IRepositories.IPrintRepo;
using PrintRelayServer.Infrastructure.Contexts;

namespace PrintRelayServer.Infrastructure.Repositories.PrintRepo;

public class PrintJobRepository:Repository<PrintJob,Guid>,IPrintJobRepository
{
    private PrintRelayContext _context;
    public PrintJobRepository(PrintRelayContext context) : base(context)
    {
        _context = context;
    }
}