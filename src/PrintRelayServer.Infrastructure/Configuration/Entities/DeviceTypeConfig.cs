using System.Xml.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintRelayServer.Domain.Entities.DeviceAgg;

namespace PrintRelayServer.Infrastructure.Configuration.Entities;

public class DeviceTypeConfig: IEntityTypeConfiguration<DeviceType>
{
    public void Configure(EntityTypeBuilder<DeviceType> builder)
    {
        builder.ToTable("DeviceTypes");
        builder.HasKey(x=>x.Id);
        
        builder.Property(x=>x.Name).HasMaxLength(128).IsRequired();
        builder.Property(x=>x.Description).HasMaxLength(512).IsRequired();

        builder.HasMany(x => x.AvailableOptions).WithOne(x => x.DeviceType)
            .HasForeignKey(x => x.DeviceTypeId);
    }
}