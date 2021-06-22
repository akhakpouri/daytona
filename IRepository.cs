using System.Threading.Tasks;
using Daytona.Models;

namespace Daytona
{
    public interface IRepository<T> : IReadOnlyRepository<T> where T : AuditableEntity
    {
        Task Delete(int id);
        void Save(T entity);
    }
}