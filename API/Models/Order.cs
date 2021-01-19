using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Order
    {
        public Order()
        {
            Id = Guid.NewGuid();
        }

        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(11)]
        public string Cpf { get; set; }

        [Required]
        public decimal TotalValue { get; set; }

        public List<OrderItem> Items { get; set; }
    }
}
