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
    public class ProductRepository : IProductRepository
    {
        private StoreContext _context;
        private ProductValidator _validator = new ProductValidator();

        public ProductRepository(StoreContext context)
        {
            //_validator = new ProductValidator();
            _context = context;
        }

        public Product GetProduct(string productName)
        {
            var product = _context.Products.First(p => p.Name == productName);
            if (product == null) return null;
            else return product;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public async Task<dynamic> AddProduct(Product product)
        {

            var result = _validator.Validate(product);
            if (result.IsValid)
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();

                // Adiciona nome do produto adicionado para fazer o controle de não poder adicionar produtos duplicados
                _validator.ProductsName.Add(product.Name);

                return product;
            }
            else return result.Errors;
        }

        public async Task<Product> DeleteProduct(Guid productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return null;
            else
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return product;
            }
        }

        public dynamic EditProduct(Product product)
        {
            var validate = _validator.Validate(product);
            if (validate.IsValid)
            {
                var id = product.Id;
                var _product = _context.Products.Find(id);
                if (_product == null) return "Produto não encontrado";
                else
                {
                    //pq nao posso fazer apenas _product = product
                    _product.Name = product.Name;
                    _product.Value = product.Value;
                    _product.Category = product.Category;

                    _context.SaveChanges();
                    return _product;
                }
            }
            else return validate.Errors;
        }

        public IEnumerable<Product> GetProductsList()
        {
             return _context.Products.ToList();
        }
    }
}
