using PrintRelayServer.Domain.Entities.DeviceAgg;
using PrintRelayServer.Domain.IRepositories.IDeviceRepo;
using PrintRelayServer.Infrastructure.Contexts;

namespace PrintRelayServer.Infrastructure.Repositories.DeviceRepo;

public class DeviceTypeRepository : Repository<DeviceType,Guid>, IDeviceTypeRepository
{
    private readonly PrintRelayContext _context;
    public DeviceTypeRepository(PrintRelayContext context) : base(context)
    {
        _context = context;
    }
}