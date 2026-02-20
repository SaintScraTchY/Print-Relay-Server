using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintRelayServer.Domain.Entities.AgentAgg;

namespace PrintRelayServer.Infrastructure.Configuration.Entities;

public class Agentdeviceconfig : IEntityTypeConfiguration<AgentDevice>
{
    public void Configure(EntityTypeBuilder<AgentDevice> builder)
    {
        builder.ToTable("AgentDevices");
        
        builder.HasKey(x => x.Id);
        
        // Composite unique index - one agent can only link to a device once
        builder.HasIndex(x => new { x.AgentId, x.DeviceId })
            .IsUnique();
        
        // Index for queries
        builder.HasIndex(x => x.DeviceId);
        builder.HasIndex(x => new { x.DeviceId, x.IsActive });
        
        // Relationships configured in ClientAgent and Device configurations
        builder.HasOne(x => x.Device)
            .WithMany(x => x.AgentDevices)
            .HasForeignKey(x => x.DeviceId);
        
        builder.ConfigureTimestamps();
    }
}