using AutoMapper;
using Pawsy.Application.Common.Interfaces;
using Pawsy.Application.Dtos;
using Pawsy.Application.Services.Interface;
using Pawsy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawsy.Application.Services.Implementation
{
    public class OrderHeaderService : IOrderHeaderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderHeaderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderHeaderDto>> GetAllAsync()
        {
            var orderHeaders = await _unitOfWork.OrderHeader.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderHeaderDto>>(orderHeaders);
        }

        public async Task<OrderHeaderDto?> GetByIdAsync(int id)
        {
            var orderHeader = await _unitOfWork.OrderHeader.GetByIdAsync(id);
            return _mapper.Map<OrderHeaderDto>(orderHeader);
        }

        public async Task<OrderHeaderDto> CreateAsync(OrderHeaderDto dto)
        {
            var entity = _mapper.Map<OrderHeader>(dto);
            await _unitOfWork.OrderHeader.AddAsync(entity);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<OrderHeaderDto>(entity);
        }

        public async Task<bool> UpdateAsync(OrderHeaderDto dto)
        {
            var entity = await _unitOfWork.OrderHeader.GetByIdAsync(dto.Id);
            if (entity == null) return false;
            _mapper.Map(dto, entity);
            _unitOfWork.OrderHeader.Update(entity);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.OrderHeader.GetByIdAsync(id);
            if (entity == null) return false;
            _unitOfWork.OrderHeader.Remove(entity);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> UpdateStatusAsync(int id, string orderStatus, string? paymentStatus = null)
        {
            var entity = await _unitOfWork.OrderHeader.GetByIdAsync(id);
            if (entity == null)
                return false;

            entity.OrderStatus = orderStatus;
            if (!string.IsNullOrEmpty(paymentStatus))
                entity.PaymentStatus = paymentStatus;

            _unitOfWork.OrderHeader.Update(entity);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> UpdateStripePaymentAsync(int id, string sessionId, string paymentIntentId)
        {
            var entity = await _unitOfWork.OrderHeader.GetByIdAsync(id);
            if (entity == null)
                return false;

            entity.SessionId = sessionId;
            entity.PaymentIntentId = paymentIntentId;

            _unitOfWork.OrderHeader.Update(entity);
            await _unitOfWork.SaveAsync();
            return true;
        }


    }
}
