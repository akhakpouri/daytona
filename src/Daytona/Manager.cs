using AutoMapper;
using Daytona.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Daytona;

public abstract class Manager<TUnitOfWork, TDto>(TUnitOfWork unitOfWork, IMapper mapper) : IManager<TUnitOfWork, TDto>
    where TDto : BaseDto
{
    protected readonly TUnitOfWork UnitOfWork = unitOfWork;
    protected readonly IMapper Mapper = mapper;

    public abstract Task Delete(int id);
    public abstract Task<List<TDto>> GetAll();
    public abstract Task<TDto> GetById(int id);
    public abstract Task<int> Save(TDto dto);
}