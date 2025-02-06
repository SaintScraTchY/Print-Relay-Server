using PrintRelayServer.Domain.Entities.DeviceAgg;
using PrintRelayServer.Domain.IRepositories.IDeviceRepo;
using PrintRelayServer.Infrastructure.Contexts;

namespace PrintRelayServer.Infrastructure.Repositories.DeviceRepo;

public class DeviceOptionValueRepository:Repository<DeviceOptionValue,Guid>,IDeviceOptionValueRepository
{
    private readonly PrintRelayContext _context;
    public DeviceOptionValueRepository(PrintRelayContext context) : base(context)
    {
        _context = context;
    }
}