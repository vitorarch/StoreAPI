using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Product
    {

        public Product()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Category { get; set; }

        [Required]
        public decimal Value { get; set; }
    }
}
