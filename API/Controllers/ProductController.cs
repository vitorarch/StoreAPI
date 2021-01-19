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
    [Route("api/Product")]

    public class ProductController : ControllerBase
    {

        private readonly IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        //[HttpGet("?Name={productName}")]
        //public IActionResult GetProduct(string productName)
        //{
        //    var request = _repository.GetProduct(productName);

        //    if (request is Product) return Ok(request);
        //    else return Problem("Produto não encontrado");
        //}

        [HttpGet]
        public IActionResult GetProduct(string productName)
        {
            var request = _repository.GetAllProducts();

            return Ok(request);
        }


        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] Product product)
        {
            var request = await _repository.AddProduct(product);

            if (request is Product) return Ok(request);
            else return Problem(request);
        }


        [HttpPut]
        public IActionResult EditProduct([FromForm] Product product)
        {
            var request = _repository.EditProduct(product);

            if (request is string) return Problem(request);
            else return Ok(request);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var request = await _repository.DeleteProduct(id);
            
            if (request is null) return Problem("Produto não encontrado");
            else return Ok($"{request.Name} deletado com sucesso");
        }
    }
}