using Daytona.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Daytona.EntityConfigurations;

public abstract class BaseSqlEntityConfiguration<TBaseCompleteEntity> : BaseEntityConfiguration<TBaseCompleteEntity>
    where TBaseCompleteEntity : BaseCompleteEntity
{
    public override void Configure(EntityTypeBuilder<TBaseCompleteEntity> builder)
    {
        builder.Property(b => b.CreatedDate)
            .HasColumnType("datetime")
            .HasDefaultValueSql("GETUTCDATE()")
            .IsRequired();
        builder.Property(b => b.UpdatedDate)
            .HasColumnType("datetime")
            .HasDefaultValueSql("GETUTCDATE()")
            .IsRequired();

        base.Configure(builder);
    }
}