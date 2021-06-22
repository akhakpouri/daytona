using Daytona.Dto;
using System.Threading.Tasks;

namespace Daytona
{
    public interface IManager<TUnitOfWork, TDto> : IReadOnlyManager<TUnitOfWork, TDto> where TDto : BaseDto
    {
        Task Delete(int id);
        Task<int> Save(TDto dto);
    }
}
