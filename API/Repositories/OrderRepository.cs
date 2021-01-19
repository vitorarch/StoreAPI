using API.DataAccess;
using API.Interfaces;
using API.Models;
using API.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        
        private StoreContext _context;
        private OrderValidator _orderValidator;

        public OrderRepository(StoreContext context)
        {
            _context = context;
            _orderValidator = new OrderValidator();
        }

        public async Task<dynamic> AddOrder(Order order)
        {
            decimal totalValue = 0;

            foreach(var x in order.Items)
            {
                x.OrderId = order.Id;
                totalValue += x.ProductValue;
            }

            order.TotalValue = totalValue;

            var result = _orderValidator.Validate(order);

            if(result.IsValid)
            {
                await _context.Orders.AddAsync(order);
                _context.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

                await _context.SaveChangesAsync();
                return order;
            }
            return result.Errors;
            //var order = _context.Orders.AddAsync(order);
        }

        public IEnumerable<Order> GetOrderList()
        {
            return _context.Orders.ToList();
        }
    }
}
