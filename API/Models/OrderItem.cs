using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class OrderItem
    {

        public OrderItem()
        {
            Id = Guid.NewGuid();
        }

        //[Key]
        public Guid Id { get; set; }

        //[Required]
        public Guid ProductId { get; set; }

        //[Required]
        [MaxLength(50)]
        public string ProductName { get; set; }

        //[Required]
        public decimal? ProductValue { get; set; }

        public Guid OrderId { get; set; }

    }


}
