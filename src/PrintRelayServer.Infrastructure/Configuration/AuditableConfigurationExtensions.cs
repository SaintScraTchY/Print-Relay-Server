using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrintRelayServer.Domain.Base;
using PrintRelayServer.Domain.Base.Contracts;

namespace PrintRelayServer.Infrastructure.Configuration;

public static class AuditableConfigurationExtensions
{
    // برای TimestampedEntity
    public static void ConfigureTimestamps<TEntity>(
        this EntityTypeBuilder<TEntity> builder) 
        where TEntity : TimestampedEntity
    {
        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");
            
        builder.Property(x => x.ModifiedAt)
            .IsRequired(false);
    }

    public static void ConfigureAuditEntity<TEntity>(
        this EntityTypeBuilder<TEntity> builder) 
        where TEntity : AuditableEntity
    {
        builder.ConfigureTimestamps();
        builder.Property(x => x.CreatedBy).IsRequired();
        builder.Property(x => x.ModifiedBy).IsRequired(false);
        
        builder.HasOne(x=>x.CreatedBy)
            .WithMany()
            .HasForeignKey(x => x.CreatedById)
            .OnDelete(DeleteBehavior.NoAction);
            
        builder.HasOne(x=>x.ModifiedBy)
            .WithMany()
            .HasForeignKey(x => x.ModifiedById)
            .OnDelete(DeleteBehavior.NoAction);
    }

    public static void ConfigureFullAuditEntity<TEntity>(
        this EntityTypeBuilder<TEntity> builder) 
        where TEntity : FullAuditableEntity
    {
        builder.ConfigureTimestamps();
        builder.Property(x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);
            
        builder.Property(x => x.DeletedAt).IsRequired(false);
        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);
        
        builder.HasOne(x=>x.CreatedBy)
            .WithMany()
            .HasForeignKey(x => x.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);
            
        builder.HasOne(x=>x.ModifiedBy)
            .WithMany()
            .HasForeignKey(x => x.ModifiedById)
            .OnDelete(DeleteBehavior.Restrict);
            
        // Global Query Filter
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
    
    public static void ConfigureActivatableEntity<TEntity>(
        this EntityTypeBuilder<TEntity> builder) 
        where TEntity : ActivatableEntity
    {
        builder.ConfigureTimestamps();
        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);
    }
}