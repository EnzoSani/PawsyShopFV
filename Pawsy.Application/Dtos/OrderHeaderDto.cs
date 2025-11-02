using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawsy.Application.Dtos
{
    public class OrderHeaderDto
    {
        public int Id { get; set; }

        // 🔹 Usuario
        public string? ApplicationUserId { get; set; }

        // 🔹 Fechas y totales
        [Required]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public DateTime? ShippingDate { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Order total must be greater than 0.")]
        public double OrderTotal { get; set; }

        // 🔹 Estado de la orden y pago
        public string? OrderStatus { get; set; } = "Pending";
        public string? PaymentStatus { get; set; } = "Pending";

        // 🔹 Envío y seguimiento
        public string? TrackingNumber { get; set; }
        public string? Carrier { get; set; }

        // 🔹 Fechas de pago
        public DateTime? PaymentDate { get; set; }
        public DateTime? PaymentDueDate { get; set; }

        // 🔹 Integración con Stripe u otro gateway
        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; }

        // 🔹 Información de contacto y dirección
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public string StreetAddress { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
