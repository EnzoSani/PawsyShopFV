using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawsy.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository Product { get; }
        ICategoryRepository Category { get; }
        IPetRepository Pet { get; }
        IOrderHeaderRepository OrderHeader { get; }
        Task SaveAsync();
    }
}
