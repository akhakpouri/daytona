using Daytona.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Daytona.EntityConfigurations;

public abstract class BaseNpsqlEntityConfiguration<TBaseCompleteEntity> : BaseEntityConfiguration<TBaseCompleteEntity>
    where TBaseCompleteEntity : BaseCompleteEntity
{
    public override void Configure(EntityTypeBuilder<TBaseCompleteEntity> builder)
    {
        builder.Property(b => b.CreatedDate)
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("NOW()")
            .IsRequired();
        builder.Property(b => b.UpdatedDate)
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        base.Configure(builder);
    }
}