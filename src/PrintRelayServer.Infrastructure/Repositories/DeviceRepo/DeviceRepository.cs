using PrintRelayServer.Domain.Entities.DeviceAgg;
using PrintRelayServer.Domain.IRepositories.IDeviceRepo;
using PrintRelayServer.Infrastructure.Contexts;

namespace PrintRelayServer.Infrastructure.Repositories.DeviceRepo;

public class DeviceRepository : Repository<Device,Guid>, IDeviceRepository
{
    private readonly PrintRelayContext _context;
    public DeviceRepository(PrintRelayContext context) : base(context)
    {
        _context = context;
    }
}