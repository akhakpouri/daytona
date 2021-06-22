using Daytona.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Daytona
{
    public  interface IReadOnlyManager<TUnitOfWork, TDto> where TDto : BaseDto
    {
        Task<TDto> GetById(int id);
        Task<List<TDto>> GetAll();
    }
}
