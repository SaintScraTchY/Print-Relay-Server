using PrintRelayServer.Domain.Entities.PrintAgg;
using PrintRelayServer.Domain.IRepositories.IPrintRepo;
using PrintRelayServer.Infrastructure.Contexts;

namespace PrintRelayServer.Infrastructure.Repositories.PrintRepo;

public class PrintJobEventRepository : Repository<PrintJobEvent>,IPrintJobEventRepository
{
    private PrintRelayContext _context;
    public PrintJobEventRepository(PrintRelayContext context) : base(context)
    {
        _context = context;
    }
}