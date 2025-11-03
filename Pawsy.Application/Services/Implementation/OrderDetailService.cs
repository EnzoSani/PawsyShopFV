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
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderDetailService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDetailDto>> GetAllAsync()
        {
            var orderDetails = await _unitOfWork.OrderDetail.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDetailDto>>(orderDetails);
        }

        public async Task<OrderDetailDto?> GetByIdAsync(int id)
        {
            var orderDetail = await _unitOfWork.OrderDetail.GetByIdAsync(id);
            return _mapper.Map<OrderDetailDto>(orderDetail);
        }

        public async Task<OrderDetailDto> CreateAsync(OrderDetailDto dto)
        {
            var entity = _mapper.Map<OrderDetail>(dto);
            await _unitOfWork.OrderDetail.AddAsync(entity);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<OrderDetailDto>(entity);
        }

        public async Task<bool> UpdateAsync(OrderDetailDto dto)
        {
            var entity = await _unitOfWork.OrderDetail.GetByIdAsync(dto.Id);
            if (entity == null) return false;
            _mapper.Map(dto, entity);
            _unitOfWork.OrderDetail.Update(entity);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.OrderDetail.GetByIdAsync(id);
            if (entity == null) return false;
            _unitOfWork.OrderDetail.Remove(entity);
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}
