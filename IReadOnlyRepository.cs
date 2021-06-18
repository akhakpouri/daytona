using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Daytona.Models;

namespace Daytona
{
    public interface IReadOnlyRepository<T> where T : AuditableEntity
    {
        Task<T> GetById(int id);
        Task<IQueryable<T>> FilterBy(Expression<Func<T, bool>> expression);
    }
}