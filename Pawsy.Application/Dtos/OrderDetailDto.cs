using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawsy.Application.Dtos
{
    public class OrderDetailDto
    {
        public int Id { get; set; }

        [Required]
        public int OrderHeaderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Count must be at least 1.")]
        public int Count { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public double Price { get; set; }

        public string? ProductName { get; set; }
        public string? ProductImageUrl { get; set; }
    }
}
