using Core.PocoEntities;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository <T> where T : BaseEntity 
    {
        public Task<T> GetById(int Id);

        public Task<IReadOnlyList<T>> GetAllListAsync();

        Task<T> GetEntityWithSpec(ISpecification<T> spec);

        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
    }
}
