using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;

namespace Infrastructure.Data
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsyncWithSpec(ISpecification<T> spec);
        Task<T> GetEntityAsyncWithSpec(ISpecification<T> spec);

        Task<int> CountAsync(ISpecification<T> spec);

    }
}