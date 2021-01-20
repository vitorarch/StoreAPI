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
            _context = context;
        }

        public Product GetProductByName(string productName)
        {
            var product = _context.Products.Where(p => p.Name == productName).FirstOrDefault();
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
            else return result.Errors[0].ErrorMessage;
        }

        public async Task<dynamic> DeleteProduct(Guid productId)
        {
            var product = await _context.Products.FindAsync(productId);
            // validar s e o produto existe
            if (product == null) return null;

            var productOrdered = _context.Itens.Where(p => p.ProductId == productId).FirstOrDefault();
            //valida se o produto ja foi vendido
            if (productOrdered != null) return "Esse produto já foi vendido";


            if (product == null) return null;
            else
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return product;
            }
        }

        public Product EditProduct(Product product)
        {
            var _product = SelectObjectChanges(product);

            if (_product == null) return null;
            else
            {
                _context.SaveChanges();
                return _product;
            }
        }

        public IEnumerable<Product> GetProductsList()
        {
             return _context.Products.ToList();
        }

        #region Auxiliar Methods

        private Product SelectObjectChanges(Product product)
        {
            var _product = _context.Products.Find(product.Id);

            if (_product == null) return null;
            else
            {
                if (!string.IsNullOrEmpty(product.Name))
                    _product.Name = product.Name;
                if (product.Value != null)
                    _product.Value = product.Value;
                if (!string.IsNullOrEmpty(product.Category))
                    _product.Category = product.Category;

                return _product;
            }
        }

        #endregion

    }
}
