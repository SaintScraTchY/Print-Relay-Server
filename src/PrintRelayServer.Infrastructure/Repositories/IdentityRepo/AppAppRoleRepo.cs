using PrintRelayServer.Domain.Entities.Identity;
using PrintRelayServer.Domain.IRepositories.IIdentityRepo;
using PrintRelayServer.Infrastructure.Contexts;

namespace PrintRelayServer.Infrastructure.Repositories.IdentityRepo;

public class AppAppRoleRepo : Repository<AppRole>,IAppRoleRepo
{
    private readonly PrintRelayContext _context;
    public AppAppRoleRepo(PrintRelayContext context) : base(context)
    {
        _context = context;
    }
}