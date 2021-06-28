using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Daytona.Models
{
    public abstract class AuditableEntity
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

    public abstract class AuditableEntityEntityConfiguration<TAuditableEntity> : IEntityTypeConfiguration<TAuditableEntity> where TAuditableEntity : AuditableEntity
    {
        public void Configure(EntityTypeBuilder<TAuditableEntity> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.CreatedBy).HasMaxLength(100).IsRequired();
            builder.Property(b => b.ModifiedBy).HasMaxLength(100);
            builder.Property(b => b.CreatedDate).HasColumnType("datetime2").IsRequired();
            builder.Property(b => b.ModifiedDate).HasColumnType("datetime2").IsRequired();
        }
    }
}