using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintRelayServer.Domain.Entities.PrintAgg;

namespace PrintRelayServer.Infrastructure.Configuration.Entities;

public class PrintJobDetailConfiguration : IEntityTypeConfiguration<PrintJobDetail>
{
    public void Configure(EntityTypeBuilder<PrintJobDetail> builder)
    {
        builder.ToTable("PrintJobDetails");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.PrinterIdentifier)
            .IsRequired()
            .HasMaxLength(255);
        
        builder.Property(x => x.PaperSizeUnit)
            .HasConversion<string>()
            .HasMaxLength(20);
        
        builder.Property(x => x.PrintPaper)
            .HasConversion<string>()
            .HasMaxLength(20);
        
        builder.Property(x => x.Priority)
            .HasConversion<string>()
            .HasMaxLength(20);
        
        builder.Property(x => x.Quality)
            .HasConversion<string>()
            .HasMaxLength(20);
        
        builder.Property(x => x.Margins)
            .HasMaxLength(50);
    }
}