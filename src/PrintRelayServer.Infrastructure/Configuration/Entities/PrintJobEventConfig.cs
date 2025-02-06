using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintRelayServer.Domain.Entities.PrintAgg;

namespace PrintRelayServer.Infrastructure.Configuration.Entities;

public class PrintJobEventConfig: IEntityTypeConfiguration<PrintJobEvent>
{
    public void Configure(EntityTypeBuilder<PrintJobEvent> builder)
    {
        builder.ToTable("PrintJobEvents");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.PrintJobId).IsRequired();
        builder.Property(x => x.QueuePosition).IsRequired();
    }
}