using Pawsy.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawsy.Application.Services.Interface
{
    public interface IOrderDetailService
    {
        Task<IEnumerable<OrderDetailDto>> GetAllAsync();
        Task<OrderDetailDto?> GetByIdAsync(int id);
        Task<OrderDetailDto> CreateAsync(OrderDetailDto dto);
        Task<bool> UpdateAsync(OrderDetailDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
