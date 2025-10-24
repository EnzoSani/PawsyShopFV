using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawsy.Domain.Entities
{
    public class Pet
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public int Age { get; private set; }
        public string? Gender { get; private set; }
        public string? ImageUrl { get; set; }
        public DateTime? Created_Date { get; set; }
        public DateTime? Updated_Date { get; set; }
        public int CategoryId { get; private set; }
        public Category Category { get; private set; } = null!;
    }
}
