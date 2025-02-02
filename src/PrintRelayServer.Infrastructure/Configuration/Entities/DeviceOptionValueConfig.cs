using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintRelayServer.Domain.Entities.DeviceAgg;

namespace PrintRelayServer.Infrastructure.Configuration.Entities;

public class DeviceOptionValueConfig : IEntityTypeConfiguration<DeviceOptionValue>
{
    public void Configure(EntityTypeBuilder<DeviceOptionValue> builder)
    {
        builder.ToTable("DeviceOptionValues");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Label).HasMaxLength(128).IsRequired();
        builder.Property(x => x.Value).HasMaxLength(256).IsRequired();

        builder.HasOne(x => x.DeviceTypeOption)
            .WithMany(x => x.AllowedOptions)
            .HasForeignKey(x => x.DeviceTypeOptionId);
        
        builder.HasOne(x => x.Creator)
            .WithMany()
            .HasForeignKey(x => x.CreatedBy);
        
        builder.HasOne(x => x.Modifier)
            .WithMany()
            .HasForeignKey(x => x.UpdatedBy);
    }
}