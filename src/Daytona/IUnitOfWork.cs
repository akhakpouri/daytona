using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Daytona
{
    public interface IUnitOfWork<TDbContext> where TDbContext : DbContext
    {
        Task Commit();
    }
}
