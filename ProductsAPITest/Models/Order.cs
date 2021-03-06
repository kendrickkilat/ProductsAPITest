using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPITest.Models
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }
        
        [Required]
        public DateTime DateOrdered { get; set; }

        [Required]
        public string OrderAddress { get; set; }

        [Required]
        public string Status { get; set; }

        public List<OrderItem> OrderItem { get; set; }
    }
}
