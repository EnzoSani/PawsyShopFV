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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _unitOfWork.Category.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await _unitOfWork.Category.GetByIdAsync(id);
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> CreateAsync(CategoryDto dto)
        {
            var entity = _mapper.Map<Category>(dto);
            await _unitOfWork.Category.AddAsync(entity);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<CategoryDto>(entity);
        }

        public async Task<bool> UpdateAsync(CategoryDto dto)
        {
            var entity = await _unitOfWork.Category.GetByIdAsync(dto.Id);
            if (entity == null) return false;
            _mapper.Map(dto, entity);
            _unitOfWork.Category.Update(entity);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.Category.GetByIdAsync(id);
            if (entity == null) return false;
            _unitOfWork.Category.Remove(entity);
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}
