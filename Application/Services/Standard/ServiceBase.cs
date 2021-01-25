using Application.Interfaces.Services.Standard;
using Domain.Entities;
using DTO.Pagination;
using Newtonsoft.Json;
using Infrastructure.Interfaces.Repositories.Standard;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AutoMapper;
using System;

namespace Application.Services.Standard
{
    public abstract class ServiceBase<TEntity, TRequestDTO, TResponseDTO> : IServiceBase<TEntity, TRequestDTO, TResponseDTO>
     where TEntity : class, IIdentityEntity
      where TRequestDTO : class, DTO.Request.IRequestDTO
       where TResponseDTO : class, DTO.Response.IResponseDTO
    {

        protected readonly ILogger<ServiceBase<TEntity, TRequestDTO, TResponseDTO>> _logger;
        protected readonly IRepositoryAsync<TEntity> repository;
        protected readonly IMapper _mapper;

        public ServiceBase(IRepositoryAsync<TEntity> repository, ILogger<ServiceBase<TEntity, TRequestDTO, TResponseDTO>> logger, IMapper mapper)
        {
            this._mapper = mapper;
            this.repository = repository;
            this._logger = logger;
        }

        public virtual async Task<TResponseDTO> AddAsync(TRequestDTO obj)
        {
            _logger.LogInformation("[AddAsync {0}] {1}", obj.GetType(), JsonConvert.SerializeObject(obj));
            var x = _mapper.Map<TEntity>(obj);
            var y = await repository.AddAsync(x);
            var z = _mapper.Map<TResponseDTO>(y);
            return z;
        }

        public virtual async Task AddRangeAsync(IEnumerable<TRequestDTO> entities)
        {
            _logger.LogInformation("[AddRangeAsync] {0}", JsonConvert.SerializeObject(entities));
            var x = _mapper.Map<IEnumerable<TEntity>>(entities);
            await repository.AddRangeAsync(x);
        }

        public virtual async Task<IEnumerable<TResponseDTO>> GetAllAsync()
        {
            var y = await repository.GetAllAsync();
            var x = _mapper.Map<IEnumerable<TResponseDTO>>(y);
            return x;
        }

        public virtual async Task<PaginatedList<TEntity, TResponseDTO>> GetAllPaginatedAsync(int? pageIndex, int? pageSize)
        {
            var listAll = await repository.GetAllAsync();
            int index = (pageIndex == null || pageIndex < 0) ? 1 : (int)pageIndex;
            int size = (pageSize == null || pageSize < 0) ? 10 : (int)pageSize;

            var list = PaginatedList<TEntity, TResponseDTO>.Create(listAll, index, size, _mapper);

            return list;
        }

        public virtual async Task<TResponseDTO> GetByIdAsync(Guid id)
        {
            _logger.LogInformation("[GetByIdAsync] {0}", id);

            var y = await repository.GetByIdAsync(id);
            var x = _mapper.Map<TResponseDTO>(y);
            return x;
        }

        public virtual async Task<bool> RemoveAsync(Guid id)
        {
            _logger.LogInformation("[RemoveAsync] {0}", id);
            return await repository.RemoveAsync(id);
        }

        public virtual async Task RemoveAsync(TEntity obj)
        {
            _logger.LogInformation("[RemoveAsync] {0}", JsonConvert.SerializeObject(obj));
            await repository.RemoveAsync(obj);
        }

        public virtual async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            _logger.LogInformation("[RemoveRangeAsync] {0}", JsonConvert.SerializeObject(entities));
            await repository.RemoveRangeAsync(entities);
        }

        public virtual async Task UpdateAsync(Guid id, TRequestDTO obj)
        {
            _logger.LogInformation("[UpdateAsync] {0}", JsonConvert.SerializeObject(obj));
            var x = _mapper.Map<TEntity>(obj);
            x.Id = id;
            await repository.UpdateAsync(x);
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            _logger.LogInformation("[UpdateRangeAsync] {0}", JsonConvert.SerializeObject(entities));
            await repository.UpdateRangeAsync(entities);
        }
    }
}
