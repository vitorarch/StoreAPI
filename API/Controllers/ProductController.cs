﻿using API.Interfaces;
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

        [HttpGet("Name/{productName}")]
        public IActionResult GetProductByName(string productName)
        {
            var request = _repository.GetProductByName(productName);

            if (request is Product) return Ok(request);
            else return BadRequest(new { error = "Produto não encontrado" });
        }

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
            else return BadRequest(new { error = request });
        }


        [HttpPut]
        public IActionResult EditProduct([FromForm] Product product)
        {
            var request = _repository.EditProduct(product);

            if (request == null) return BadRequest(new { error = "Produto não encontrado" });
            else return Ok(request);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var request = await _repository.DeleteProduct(id);
            
            if (request is string) return BadRequest(new { error = request });
            else if (request == null) return BadRequest(new { error = "Produto não encontrado" });
            else return Ok(new { message = $"{request.Name} deletado com sucesso" });
        }
    }
}