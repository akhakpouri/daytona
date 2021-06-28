using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Daytona.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Daytona
{
    public class ReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity>
        where TEntity : AuditableEntity
    {
        protected DbContext Context { get; private set; }

        public ReadOnlyRepository(DbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<TEntity> GetById(int id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            var set = Set();
            if (include != null)
                set = include(set);
            return await set.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
        }

        public IQueryable<TEntity> FilterBy(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            var set = Set();
            if (include != null)
                set = include(set);

            return set.Where(expression);
        }

        private IQueryable<TEntity> Set()
        {
            return Context.Set<TEntity>();
        }
    }
}