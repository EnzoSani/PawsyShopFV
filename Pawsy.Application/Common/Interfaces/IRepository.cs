using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawsy.Application.Common.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(string? includeProperties = null);
        Task<T?> GetByIdAsync(int id, string? includeProperties = null);
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
