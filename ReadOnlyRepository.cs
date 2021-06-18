using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Daytona.Models;
using Microsoft.EntityFrameworkCore;

namespace Daytona
{
    public class ReadOnlyRepository<TEntity, TContext> : IReadOnlyRepository<TEntity> 
        where TEntity : AuditableEntity
        where TContext : DbContext
    {
        private readonly TContext _context;

        public ReadOnlyRepository(TContext context)
        {
            _context = context;
        }
        
        public async Task<TEntity> GetById(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IQueryable<TEntity>> FilterBy(Expression<Func<TEntity, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}