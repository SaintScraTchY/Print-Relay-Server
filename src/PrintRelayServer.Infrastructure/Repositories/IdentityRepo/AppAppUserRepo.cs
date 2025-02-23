using PrintRelayServer.Domain.Entities.Identity;
using PrintRelayServer.Domain.IRepositories.IIDentityRepo;
using PrintRelayServer.Infrastructure.Contexts;

namespace PrintRelayServer.Infrastructure.Repositories.IdentityRepo;

public class AppAppUserRepo : Repository<AppUser>, IAppUserRepo
{
    private readonly PrintRelayContext _context;
    public AppAppUserRepo(PrintRelayContext context) : base(context)
    {
        _context = context;
    }
}