using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintRelayServer.Domain.Entities.PrintAgg;

namespace PrintRelayServer.Infrastructure.Configuration.Entities;

public class PrintJobConfig : IEntityTypeConfiguration<PrintJob>
{
    public void Configure(EntityTypeBuilder<PrintJob> builder)
    {
        builder.ToTable("PrintJobs");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.DetailId).IsRequired();
        builder.Property(x => x.DeviceId).IsRequired();
        
        builder.ConfigureTimestamps();
        builder.ConfigureAuditEntity();
    }
}