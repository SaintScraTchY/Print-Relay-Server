using PrintRelayServer.Domain.Entities.DeviceAgg;
using PrintRelayServer.Domain.IRepositories.IDeviceRepo;
using PrintRelayServer.Infrastructure.Contexts;

namespace PrintRelayServer.Infrastructure.Repositories.DeviceRepo;

public class DeviceTypeOptionRepository : Repository<DeviceTypeOption,Guid>, IDeviceTypeOptionRepository
{
    private readonly PrintRelayContext _context;
    public DeviceTypeOptionRepository(PrintRelayContext context) : base(context)
    {
        this._context = context;
    }
}