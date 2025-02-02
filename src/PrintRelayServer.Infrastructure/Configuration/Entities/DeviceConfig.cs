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
        builder.Property(x => x.Code).HasMaxLength(32).IsRequired(false);
        builder.Property(x => x.DeviceTypeId).IsRequired();
        builder.Property(x => x.OwnerId).IsRequired();

        builder.HasOne(x => x.DeviceType)
            .WithMany(x => x.Devices)
            .HasForeignKey(x => x.DeviceTypeId);

        builder.HasOne(x => x.Owner)
            .WithMany(x => x.Devices)
            .HasForeignKey(x => x.OwnerId);
        
        builder.HasOne(x => x.Creator)
            .WithMany()
            .HasForeignKey(x => x.CreatedBy);
        
        builder.HasOne(x => x.Modifier)
            .WithMany()
            .HasForeignKey(x => x.UpdatedBy);
    }
}