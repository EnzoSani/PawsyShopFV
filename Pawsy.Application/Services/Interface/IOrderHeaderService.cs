using Pawsy.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawsy.Application.Services.Interface
{
    public interface IOrderHeaderService
    {
        Task<OrderHeaderDto> CreateAsync(OrderHeaderDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<OrderHeaderDto>> GetAllAsync();
        Task<OrderHeaderDto?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(OrderHeaderDto dto);
        Task<bool> UpdateStatusAsync(int id, string orderStatus, string? paymentStatus = null);
        Task<bool> UpdateStripePaymentAsync(int id, string sessionId, string paymentIntentId);
    }
}
