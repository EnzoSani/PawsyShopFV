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
        public int Age { get;  set; }
        public string? Gender { get;  set; }
        public string? ImageUrl { get; set; }
        public DateTime? Created_Date { get; set; }
        public DateTime? Updated_Date { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
