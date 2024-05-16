using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Daytona.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Daytona;

public class Repository<TEntity>(DbContext context) : ReadOnlyRepository<TEntity>(context), IRepository<TEntity>
    where TEntity : BaseEntity
{
    public async Task BulkDelete(Expression<Func<TEntity, bool>> expression)
    {
        await Context.Set<TEntity>()
            .Where(expression)
            .ExecuteDeleteAsync();
    }

    public async Task BulkUpdate(Expression<Func<TEntity, bool>> expression,
        Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setter)
    {
        await Context.Set<TEntity>()
            .Where(expression)
            .ExecuteUpdateAsync(setter);
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