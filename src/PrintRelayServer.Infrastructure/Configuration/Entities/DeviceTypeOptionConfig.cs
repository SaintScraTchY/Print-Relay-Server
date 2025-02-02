using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintRelayServer.Domain.Entities.DeviceAgg;

namespace PrintRelayServer.Infrastructure.Configuration.Entities;

public class DeviceTypeOptionConfig : IEntityTypeConfiguration<DeviceTypeOption>
{
    public void Configure(EntityTypeBuilder<DeviceTypeOption> builder)
    {
        builder.ToTable("DeviceTypeOptions");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.OptionName).HasMaxLength(128).IsRequired();
        builder.Property(x => x.DeviceTypeId).IsRequired();

        builder.HasMany(x => x.AllowedOptions)
            .WithOne(x => x.DeviceTypeOption)
            .HasForeignKey(x => x.DeviceTypeOptionId);
        
        builder.HasOne(x => x.Creator)
            .WithMany()
            .HasForeignKey(x => x.CreatedBy);
        
        builder.HasOne(x => x.Modifier)
            .WithMany()
            .HasForeignKey(x => x.UpdatedBy);
        
    }
}