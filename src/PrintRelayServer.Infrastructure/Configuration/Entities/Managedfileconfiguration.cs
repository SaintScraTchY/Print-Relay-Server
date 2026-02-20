using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintRelayServer.Domain.Entities.FileAgg;

namespace PrintRelayServer.Infrastructure.Configuration.Entities;

public class ManagedFileConfiguration : IEntityTypeConfiguration<ManagedFile>
{
    public void Configure(EntityTypeBuilder<ManagedFile> builder)
    {
        builder.ToTable("ManagedFiles");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.FileName)
            .IsRequired()
            .HasMaxLength(255);
        
        builder.Property(x => x.Path)
            .IsRequired()
            .HasMaxLength(500);
        
        builder.Property(x => x.ContentType)
            .HasMaxLength(100);
        
        builder.Property(x => x.Hash)
            .HasMaxLength(64); // SHA256 hex string length
        
        // Indexes
        builder.HasIndex(x => x.CreatedById);
        builder.HasIndex(x => x.Hash)
            .HasFilter("\"Hash\" IS NOT NULL"); // For file deduplication
        
        builder.ConfigureFullAuditEntity();
        
        builder.HasMany(x => x.PrintJobs)
            .WithOne(x => x.File)
            .HasForeignKey(x => x.FileId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}