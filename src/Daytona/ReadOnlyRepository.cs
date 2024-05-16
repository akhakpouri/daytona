#nullable enable
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Daytona.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Daytona;

public class ReadOnlyRepository<TEntity>(DbContext context) : IReadOnlyRepository<TEntity>
    where TEntity : BaseEntity
{
    protected DbContext Context { get; } = context ?? throw new ArgumentNullException(nameof(context));

    public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        var set = Set();
        if (include != null)
            set = include(set);

        return set.Where(expression);
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
    {
        var set = Set();
        return await set.AnyAsync(expression);
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        var set = Set();
        if (include != null)
            set = include(set);
        return await set.Where(expression).CountAsync();
    }

    public async Task<TEntity?> GetById(int id,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        var set = Set();
        if (include != null)
            set = include(set);
        return await set.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
    }

    IQueryable<TEntity> Set()
        => Context.Set<TEntity>();
}