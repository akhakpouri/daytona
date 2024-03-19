using System.Threading.Tasks;
using Daytona.Entities;

namespace Daytona
{
    public interface IRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : BaseEntity
    {
        Task Delete(int id);
        void Save(TEntity entity);
    }
}