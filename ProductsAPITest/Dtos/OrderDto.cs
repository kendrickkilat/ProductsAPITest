﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPITest.Dtos
{
    public class OrderDto
    {
        [Key]
        public Guid id { get; set; }

        [Required]
        public string OrderAddress { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
