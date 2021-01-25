using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTO.Pagination; 
using Newtonsoft.Json;

namespace Application.Interfaces.Services.Standard
{
    public interface IServiceBase<TEntity, TRequestDTO, TResponseDTO> 
    where TEntity : class
    where TRequestDTO : class
    where TResponseDTO : class
    {
        Task<TResponseDTO> AddAsync(TRequestDTO obj);
        //Task AddRangeAsync(IEnumerable<TEntity> entities);

        Task<TResponseDTO> GetByIdAsync(Guid id);
        Task<IEnumerable<TResponseDTO>> GetAllAsync();
        Task<PaginatedList<TEntity, TResponseDTO>> GetAllPaginatedAsync(int? pageIndex, int? pageSize);

        Task UpdateAsync(Guid id, TRequestDTO obj);
        //Task UpdateRangeAsync(IEnumerable<TRequestDTO> entities);

        Task<bool> RemoveAsync(Guid id);
        //Task RemoveAsync(TEntity obj);
        //Task RemoveRangeAsync(IEnumerable<TEntity> entities);
    }
}