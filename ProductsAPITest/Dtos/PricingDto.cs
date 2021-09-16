﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPITest.Dtos
{
    public class PricingDto
    {
        [Key]
        public Guid id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public float Price { get; set; }
    }
}