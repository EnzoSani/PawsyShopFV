using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawsy.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int Stock { get; set; } = 0; 

        [StringLength(100)]
        public string? Brand { get; set; }  // Ej: “Royal Canin”, “Pedigree”
        [Required]
        [Range(1, 10000)]
        [Display(Name = "List Price")]
        public double ListPrice { get; set; }
        [Required]
        [Range(1, 10000)]
        [Display(Name = "Price 1-50")]
        public double Price { get; set; }
        [Required]
        [Range(1, 10000)]
        [Display(Name = "Price 51- 100")]
        public double Price50 { get; set; }
        [Required]
        [Range(1, 10000)]
        [Display(Name = "Price 100+")]
        public double Price100 { get; set; }
        
        public string ImageUrl { get; set; }
        public bool IsFeatured { get; set; } = false; // productos destacados en Home
        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        
        public Category Category { get; set; }
        [Required]
        [Display(Name = "Pet")]
        public int PetId { get; set; }
        
        public Pet Pet { get; set; }
    }
}
