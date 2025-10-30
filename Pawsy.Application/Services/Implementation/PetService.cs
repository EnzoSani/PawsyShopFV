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
    public class PetService : IPetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PetService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PetDto>> GetAllAsync()
        {
            var pets = await _unitOfWork.Pet.GetAllAsync();
            return _mapper.Map<IEnumerable<PetDto>>(pets);
        }

        public async Task<PetDto?> GetByIdAsync(int id)
        {
            var pet = await _unitOfWork.Pet.GetByIdAsync(id);
            return _mapper.Map<PetDto>(pet);
        }

        public async Task<PetDto> CreateAsync(PetDto dto)
        {
            var entity = _mapper.Map<Pet>(dto);
            await _unitOfWork.Pet.AddAsync(entity);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<PetDto>(entity);
        }

        public async Task<bool> UpdateAsync(PetDto dto)
        {
            var entity = await _unitOfWork.Pet.GetByIdAsync(dto.Id);
            if (entity == null) return false;
            _mapper.Map(dto, entity);
            _unitOfWork.Pet.Update(entity);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.Pet.GetByIdAsync(id);
            if (entity == null) return false;
            _unitOfWork.Pet.Remove(entity);
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}
