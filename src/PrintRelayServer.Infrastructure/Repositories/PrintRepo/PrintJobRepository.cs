using Microsoft.EntityFrameworkCore;
using PrintRelayServer.Domain.Entities.PrintAgg;
using PrintRelayServer.Domain.IRepositories.IPrintRepo;
using PrintRelayServer.Infrastructure.Contexts;
using PrintRelayServer.Shared.Contracts.Base;
using PrintRelayServer.Shared.Contracts.PrintJob;

namespace PrintRelayServer.Infrastructure.Repositories.PrintRepo;

public class PrintJobRepository : Repository<PrintJob>, IPrintJobRepository
{
    private readonly PrintRelayContext _context;
    private readonly DbSet<PrintJob> _dbset;
    public PrintJobRepository(PrintRelayContext context, DbSet<PrintJob> dbset) : base(context)
    {
        _context = context;
        _dbset = dbset;
    }
}