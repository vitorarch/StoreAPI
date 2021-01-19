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
        //private List<Guid> _productsOrded;
        private StoreContext _context;
        private OrderValidator _orderValidator;

        public OrderRepository(StoreContext context)
        {
            _context = context;
            _orderValidator = new OrderValidator();
            //_productsOrded = new List<Guid>();
        }

        public async Task<dynamic> AddOrder(Order order)
        {
            decimal totalValue = 0;

            foreach(var x in order.Items)
            {
                x.OrderId = order.Id;
                totalValue += (decimal)x.ProductValue;
                //productsOrded.Add(x.ProductId);
            }

            order.TotalValue = totalValue;

            var result = _orderValidator.Validate(order);

            if(result.IsValid)
            {
                await _context.Itens.AddRangeAsync(order.Items);
                await _context.Orders.AddAsync(order);

                await _context.SaveChangesAsync();
                return order;
            }
            return result.Errors;
            //var order = _context.Orders.AddAsync(order);
        }

        public IEnumerable<dynamic> GetOrderList()
        {
            List<dynamic> list = new List<dynamic>(); 
            var x = _context.Orders.ToList();
            foreach(var y in x)
            {
                list.Add(new
                {
                    Cpf = y.Cpf,
                    TotalValue = y.TotalValue
                });
            }
            return list;
        }
    }
}
