using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PrintRelayServer.Domain.Entities.AgentAgg;
using PrintRelayServer.Domain.Entities.DeviceAgg;
using PrintRelayServer.Domain.Entities.FileAgg;
using PrintRelayServer.Domain.Entities.Identity;
using PrintRelayServer.Domain.Entities.PrintAgg;
using PrintRelayServer.Infrastructure.Configuration.Entities;

namespace PrintRelayServer.Infrastructure.Contexts;

public class PrintRelayContext : IdentityDbContext<AppUser,AppRole,Guid>
{
    public PrintRelayContext(DbContextOptions<PrintRelayContext> options):base(options)
    {
        
    }

    public DbSet<ClientAgent> ClientAgents => Set<ClientAgent>();
    public DbSet<AgentDevice> AgentDevices => Set<AgentDevice>();
    public DbSet<Device> Devices => Set<Device>();
    public DbSet<DeviceType> DeviceTypes => Set<DeviceType>();
    public DbSet<DeviceTypeOption> DeviceTypeOptions => Set<DeviceTypeOption>();
    public DbSet<DeviceOptionValue> DeviceOptionValues => Set<DeviceOptionValue>();
    public DbSet<ManagedFile> ManagedFiles => Set<ManagedFile>();
    public DbSet<PrintJob> PrintJobs => Set<PrintJob>();
    public DbSet<PrintJobDetail> PrintJobDetails => Set<PrintJobDetail>();
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(DeviceConfig).Assembly);
    
        ConfigurePostgreSql(builder); // ✅ Make sure this is called!
    
        base.OnModelCreating(builder);
    }

    private void ConfigurePostgreSql(ModelBuilder modelBuilder)
    {
        // Configure timestamp behavior for PostgreSQL
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                {
                    // Store all DateTime as UTC without timezone info
                    property.SetColumnType("timestamp without time zone");
                
                    // ✅ Add PostgreSQL default for required DateTime columns
                    if (property.ClrType == typeof(DateTime) && 
                        (property.Name == "CreatedAt" || property.Name == "CreatedOn"))
                    {
                        property.SetDefaultValueSql("NOW()");
                    }
                }
            }
        }
    }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // You can add audit trail logic here if needed
        // For now, just call base implementation
        return base.SaveChangesAsync(cancellationToken);
    }
}