using System.Linq;
using Daytona.Entities;

namespace Daytona;

public interface IReadOnlyQueryRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : BaseEntity
{
    IQueryable<TEntity> ExecuteFunction(string functionName);
}