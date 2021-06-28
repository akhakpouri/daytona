using Daytona.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Daytona
{
    public class Repository<TEntity> : ReadOnlyRepository<TEntity>, IRepository<TEntity>
        where TEntity : AuditableEntity
    {

        public Repository(DbContext context) : base(context) { }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            if (entity == null) return;
            Context.Remove(entity);
        }

        public void Save(TEntity entity)
        {
            if (entity.Id == 0)
                Context.Set<TEntity>().Add(entity);
            else
                Context.Entry(entity).State = EntityState.Modified;
        }
    }
}