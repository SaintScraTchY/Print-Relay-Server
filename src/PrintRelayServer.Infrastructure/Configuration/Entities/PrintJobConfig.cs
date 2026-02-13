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
        
        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<string>() // Store enum as string for readability
            .HasMaxLength(20);
        
        builder.Property(x => x.ErrorMessage)
            .HasMaxLength(1000);
        
        // Indexes for common queries
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.CreatedBy);
        builder.HasIndex(x => x.DeviceId);
        builder.HasIndex(x => x.AssignedAgentId);
        builder.HasIndex(x => new { x.Status, x.DeviceId }); // For finding pending jobs per device
        builder.HasIndex(x => new { x.Status, x.AssignedAgentId }); // For finding jobs per agent

        builder.HasOne(x => x.Device)
            .WithMany(x => x.PrintJobs)
            .HasForeignKey(x => x.DeviceId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(x => x.File)
            .WithMany(x => x.PrintJobs)
            .HasForeignKey(x => x.FileId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(x => x.Detail)
            .WithOne()
            .HasForeignKey<PrintJob>(x => x.DetailId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.AssignedAgent)
            .WithMany(x => x.AssignedJobs)
            .HasForeignKey(x => x.AssignedAgentId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.ConfigureAuditEntity();
    }
}