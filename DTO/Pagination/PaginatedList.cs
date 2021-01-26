using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DTO.Pagination
{
    public class PaginatedList<TEntity, TResponseDTO>
     where TEntity : class
        where TResponseDTO : class
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public List<TResponseDTO> Result { get; private set; } = new List<TResponseDTO>();

        public PaginatedList()
        {
        }

        public PaginatedList(List<TResponseDTO> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            PageSize = pageSize;

            this.Result.AddRange(items);
        }

        public static PaginatedList<TEntity, TResponseDTO> Create(IEnumerable<TEntity> source, int pageIndex, int pageSize, IMapper mapper)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            var responseList = mapper.Map<List<TEntity>, List<TResponseDTO>>(items);

            return new PaginatedList<TEntity, TResponseDTO>(responseList, count, pageIndex, pageSize);
        }
    }
}