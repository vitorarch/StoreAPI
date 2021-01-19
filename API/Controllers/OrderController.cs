using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/Order")]

    public class OrderController : ControllerBase
    {

        private readonly IOrderRepository _repository;

        public OrderController(IOrderRepository repository)
        {
            _repository = repository;
        }


        [HttpPost]
        public async Task<IActionResult> GetOrderList([FromBody] Order order)
        {
            var request = await _repository.AddOrder(order);
            
            if (request is Order) return Ok(request);
            else return Problem(request[0]);
        }


        [HttpGet]
        public IActionResult GetAllOrder()
        {
            var request = _repository.GetOrderList();
            return Ok(request);
        }
    }
}
