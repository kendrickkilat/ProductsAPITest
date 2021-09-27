using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPITest.Dtos
{
    public class OrderItemDto
    {
        [Key]
        public Guid OrderItemId { get; set; }

        [Required]
        public Guid OrderId { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public float Price { get; set; }

        public ProductDto Products { get; set; }
    }
}
