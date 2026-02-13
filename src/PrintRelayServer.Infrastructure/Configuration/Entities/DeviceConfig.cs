using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintRelayServer.Domain.Entities.DeviceAgg;

namespace PrintRelayServer.Infrastructure.Configuration.Entities;

public class DeviceConfig : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> builder)
    {
        builder.ToTable("Devices");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(256).IsRequired();
        builder.Property(x => x.OsIdentifier).HasMaxLength(512).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(1024).IsRequired(false);
        builder.Property(x => x.DeviceTypeId).IsRequired();
        builder.Property(x => x.OwnerId).IsRequired();

        // Indexes
        builder.HasIndex(x => x.OwnerId);
        builder.HasIndex(x => x.DeviceTypeId);
        builder.HasIndex(x => x.Name);
        
        // Relationships
        builder.HasOne(x => x.Owner)
            .WithMany()
            .HasForeignKey(x => x.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(x => x.DeviceType)
            .WithMany(x => x.Devices)
            .HasForeignKey(x => x.DeviceTypeId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(x => x.AgentDevices)
            .WithOne(x => x.Device)
            .HasForeignKey(x => x.DeviceId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.PrintJobs)
            .WithOne(x => x.Device)
            .HasForeignKey(x => x.DeviceId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.ConfigureFullAuditEntity();
    }
}