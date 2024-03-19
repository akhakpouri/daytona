#nullable enable
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Daytona.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Daytona
{
    public interface IReadOnlyRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity?> GetById(int id,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);

        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);
    }
}