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
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductDto> CreateAsync(ProductDto dto)
        {
            var entity = _mapper.Map<Product>(dto);
            await _unitOfWork.Product.AddAsync(entity);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<ProductDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.Product.GetByIdAsync(id);
            if (entity == null) return false;
            _unitOfWork.Product.Remove(entity);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _unitOfWork.Product.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _unitOfWork.Product.GetByIdAsync(id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<bool> UpdateAsync(ProductDto dto)
        {
            var entity = await _unitOfWork.Product.GetByIdAsync(dto.Id);
            if (entity == null) return false;
            _mapper.Map(dto, entity);
            _unitOfWork.Product.Update(entity);
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}
