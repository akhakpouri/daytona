using System.Threading.Tasks;
using Daytona.Models;

namespace Daytona
{
    public interface IRepository<T> : IReadOnlyRepository<T> where T : AuditableEntity
    {
        void Delete(int id);
        Task<int> Save(T entity);
    }
}