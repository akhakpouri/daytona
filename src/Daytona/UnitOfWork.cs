using Daytona.Models;
using Daytona.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Daytona
{
    public class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext>
        where TDbContext : DbContext
    {
        protected TDbContext Context { get; private set; }
        protected IUserAccessor UserAccessor { get; set; }

        protected UnitOfWork(TDbContext context)
        {
            Context = context;
        }

        public async Task Commit()
        {
            foreach (var entry in Context.ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                AuditableCommitHook(entry);
            }

            await Context.SaveChangesAsync();
        }

        protected void AuditableCommitHook(EntityEntry entry)
        {
            if (entry.Entity is not AuditableEntity entity) return;

            entity.ModifiedBy = UserAccessor?.UserName ?? "_unknown";
            entity.ModifiedDate = DateTime.UtcNow;

            if (entry.State == EntityState.Added)
            {
                entity.CreatedBy = entity.ModifiedBy;
                entity.CreatedDate = entity.ModifiedDate;
            }
        }
    }
}
