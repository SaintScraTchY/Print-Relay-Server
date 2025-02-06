using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintRelayServer.Domain.Entities.PrintAgg;

namespace PrintRelayServer.Infrastructure.Configuration.Entities;

public class PrintJobDetailConfig : IEntityTypeConfiguration<PrintJobDetail>
{
    public void Configure(EntityTypeBuilder<PrintJobDetail> builder)
    {
        builder.ToTable("PrintJobDetails");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.PrintPaper).IsRequired();
        
        builder.Property(x => x.Copies).IsRequired().HasDefaultValue(1);

        builder.Property(x => x.PrintPaper).IsRequired();
        builder.Property(x => x.Quality).IsRequired();
        builder.Property(x => x.PrinterIdentifier).IsRequired();
    }
}