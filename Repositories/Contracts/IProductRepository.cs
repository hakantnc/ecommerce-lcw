using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        IQueryable<Product> GetAllProducts(bool trackChanges);
        public Product GetOneProductsById(int id, bool trackChanges);
      void CreateOneProduct(Product product);
      void UpdateOneProduct(int id, Product product, bool trackChanges);
      void DeleteOneProduct(int id, Product product);
      
    }
}
