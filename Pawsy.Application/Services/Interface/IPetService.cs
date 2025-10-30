using Pawsy.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawsy.Application.Services.Interface
{
    public interface IPetService
    {
        Task<IEnumerable<PetDto>> GetAllAsync();
        Task<PetDto?> GetByIdAsync(int id);
        Task<PetDto> CreateAsync(PetDto dto);
        Task<bool> UpdateAsync(PetDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
