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

        #region Controller Logic
        public async Task<dynamic> AddOrder(Order order)
        {
            CreatingRelationBetweenOrderAndOrderItem(order);

            var result = _orderValidator.Validate(order);

            if(result.IsValid)
            {
                await _context.Itens.AddRangeAsync(order.Items);
                await _context.Orders.AddAsync(order);

                await _context.SaveChangesAsync();
                return order;
            }
            return result.Errors;
        }

        public dynamic GetOrderList()
        {
            var list = GetCpfAndTotalValueAttributes();
            return list;
        }

        #endregion

        #region Auxiliar Methods

        private dynamic GetCpfAndTotalValueAttributes()
        {
            List<dynamic> list = new List<dynamic>();
            var order = _context.Orders.ToList();

            foreach (var attribute in order)
            {
                list.Add(new
                {
                    Cpf = attribute.Cpf,
                    TotalValue = attribute.TotalValue
                });
            }
            return list;
        }

        private void CreatingRelationBetweenOrderAndOrderItem(Order order)
        {
            decimal totalValue = 0;

            foreach (var x in order.Items)
            {
                x.OrderId = order.Id;
                totalValue += (decimal)x.ProductValue;
            }
            order.TotalValue = totalValue;
        }

        #endregion
    }
}
