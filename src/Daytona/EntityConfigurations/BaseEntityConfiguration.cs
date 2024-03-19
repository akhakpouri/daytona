using Daytona.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Daytona.EntityConfigurations;

public abstract class BaseEntityConfiguration<TBaseEntity> : IEntityTypeConfiguration<TBaseEntity> where TBaseEntity : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TBaseEntity> builder)
    {
        builder.HasKey(b => b.Id);
    }
}