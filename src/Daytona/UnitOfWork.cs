using System;
using System.Linq;
using System.Threading.Tasks;
using Daytona.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Daytona
{
    public class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext>
        where TDbContext : DbContext
    {
        protected TDbContext Context { get; set; }
        protected UnitOfWork(TDbContext context)
        {
            Context = context;
        }

        public async Task Commit()
        {
            foreach (var entry in Context.ChangeTracker.Entries().Where(e => e.State is EntityState.Modified))
            {
                AuditableCommitHook(entry);
            }
            await Context.SaveChangesAsync();
        }

        private static void AuditableCommitHook(EntityEntry entry)
        {
            if (entry.Entity is not BaseCompleteEntity entity) return;
            
            entity.UpdatedDate = DateTime.UtcNow;
        }
    }
}
