using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Daytona.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Daytona
{
    public interface IRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : BaseEntity
    {
        Task BulkDelete(Expression<Func<TEntity, bool>> expression);
        Task BulkUpdate(Expression<Func<TEntity, bool>> expression,
            Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setter);
        Task Delete(int id);
        void Save(TEntity entity);
    }
}