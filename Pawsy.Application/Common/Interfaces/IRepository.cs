using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawsy.Application.Common.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        bool Any(System.Linq.Expressions.Expression<Func<T, bool>> filter);
        T Get(System.Linq.Expressions.Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false);
        IEnumerable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>>? filter = null, string? includeProperties = null, bool tracked = false);
        void Remove(T entity);
    }
}
