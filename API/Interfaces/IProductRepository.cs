using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IProductRepository
    {
        public Product GetProductByName(string productName);
        public IEnumerable<Product> GetAllProducts();
        public Task<dynamic> AddProduct(Product product);
        public Product EditProduct(Product product);
        public Task<dynamic> DeleteProduct(Guid id);
        public IEnumerable<Product> GetProductsList();

    }
}
