using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPITest.Models
{
    public class OrderItem
    {
        [Key]
        public Guid id { get; set; }

        [Required]
        public Guid OrderId { get; set; }
        
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public decimal Price { get; set; }

        public Product products { get; set; }

    }
}
