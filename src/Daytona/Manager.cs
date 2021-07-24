using AutoMapper;
using Daytona.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Daytona
{
    public abstract class Manager<TUnitOfWork, TDto> : IManager<TUnitOfWork, TDto> where TDto : BaseDto
    {
        protected readonly TUnitOfWork UnitOfWork;
        protected readonly IMapper Mapper;

        public Manager(TUnitOfWork unitOfWork, IMapper mapper) 
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        public abstract Task Delete(int id);
        public abstract Task<List<TDto>> GetAll();
        public abstract Task<TDto> GetById(int id);
        public abstract Task<int> Save(TDto dto);
    }
}