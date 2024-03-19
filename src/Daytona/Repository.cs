using System.Threading.Tasks;
using Daytona.Entities;
using Microsoft.EntityFrameworkCore;

namespace Daytona;

public class Repository<TEntity> : ReadOnlyRepository<TEntity>, IRepository<TEntity>
    where TEntity : BaseEntity
{
    public Repository(DbContext context) : base(context)
    {
    }

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