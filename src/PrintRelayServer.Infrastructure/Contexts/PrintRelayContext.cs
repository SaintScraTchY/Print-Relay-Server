using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PrintRelayServer.Domain.Entities.DeviceAgg;
using PrintRelayServer.Domain.Entities.Identity;
using PrintRelayServer.Domain.Entities.PrintAgg;
using PrintRelayServer.Infrastructure.Configuration.Entities;

namespace PrintRelayServer.Infrastructure.Contexts;

public class PrintRelayContext : IdentityDbContext<AppUser,AppRole,Guid>
{
    public PrintRelayContext(DbContextOptions<PrintRelayContext> options):base(options)
    {
        
    }

    public DbSet<Device> Devices { get; set; }
    public DbSet<DeviceType> DeviceTypes { get; set; }
    public DbSet<DeviceTypeOption> DeviceTypeOptions { get; set; }
    public DbSet<DeviceOptionValue> DeviceOptionValues { get; set; }
    public DbSet<PrintJob> PrintJobs { get; set; }
    public DbSet<PrintJobDetail> PrintJobDetails { get; set; }
    public DbSet<PrintJobEvent> PrintJobEvents { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(DeviceConfig).Assembly);
        base.OnModelCreating(builder);
    }
}