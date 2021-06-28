using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Daytona.Models;
using Microsoft.EntityFrameworkCore.Query;

namespace Daytona
{
    public interface IReadOnlyRepository<TEntity> where TEntity : AuditableEntity
    {
        Task<TEntity> GetById(int id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
        IQueryable<TEntity> FilterBy(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
    }
}