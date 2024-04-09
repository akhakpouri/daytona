using System.Linq;
using Daytona.Entities;
using Microsoft.EntityFrameworkCore;

namespace Daytona;

public class ReadOnlyQueryRepository<TEntity>(DbContext context)
    : ReadOnlyRepository<TEntity>(context), IReadOnlyQueryRepository<TEntity>
    where TEntity : BaseEntity
{
    public IQueryable<TEntity> ExecuteFunction(string functionName)
    {
        return Context.Set<TEntity>().FromSqlInterpolated($"select * from {functionName}");
    }
}