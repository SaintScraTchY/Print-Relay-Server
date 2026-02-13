using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintRelayServer.Domain.Entities.AgentAgg;

namespace PrintRelayServer.Infrastructure.Configuration.Entities;

public class ClientAgentConfig : IEntityTypeConfiguration<ClientAgent>
{
    public void Configure(EntityTypeBuilder<ClientAgent> builder)
    {
        builder.ToTable("ClientAgents");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(x => x.MachineId)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(x => x.SignalRConnectionId)
            .HasMaxLength(100);
        
        builder.Property(x => x.HostName)
            .HasMaxLength(255);
        
        builder.Property(x => x.IpAddress)
            .HasMaxLength(45); // IPv6 max length
        
        builder.Property(x => x.Version)
            .HasMaxLength(50);
        
        builder.Property(x => x.Platform)
            .HasMaxLength(50);
        
        // Indexes for performance
        builder.HasIndex(x => x.MachineId)
            .IsUnique();
        
        builder.HasIndex(x => x.SignalRConnectionId)
            .HasFilter("\"SignalRConnectionId\" IS NOT NULL");
        
        builder.HasIndex(x => x.IsConnected);
        
        builder.HasIndex(x => x.OwnerId);
        
        // Relationships
        builder.HasOne(x => x.Owner)
            .WithMany()
            .HasForeignKey(x => x.OwnerId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasMany(x => x.AgentDevices)
            .WithOne(x => x.Agent)
            .HasForeignKey(x => x.AgentId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.AssignedJobs)
            .WithOne(x => x.AssignedAgent)
            .HasForeignKey(x => x.AssignedAgentId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.ConfigureTimestamps();
    }
}