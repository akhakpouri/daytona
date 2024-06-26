﻿using AutoMapper;
using Daytona.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Daytona
{
    public abstract class ReadOnlyManager<TUnitOfWork, TDto>(IMapper mapper, TUnitOfWork unitOfWork)
        : IReadOnlyManager<TUnitOfWork, TDto>
        where TDto : BaseDto
    {
        protected readonly IMapper _mapper = mapper;
        protected readonly TUnitOfWork _unitOfWork = unitOfWork;

        public abstract Task<List<TDto>> GetAll();

        public abstract Task<TDto> GetById(int id);
    }
}
