using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawsy.Application.Dtos
{
    public class PetDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Age { get; set; }
        public string? Gender { get; set; }
        public string? ImageUrl { get; set; }
    }
}
