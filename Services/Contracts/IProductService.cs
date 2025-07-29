using Entities.DTO;
using Entities.DTO.ProductDTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> SearchProductsAsync(string query);
        IEnumerable<Entities.Models.Product> GetAllProducts(bool trackChanges);
        Product GetOneProductsById(int id, bool trackChanges);
        Product CreateOneProduct(Product product);
        void  UpdateOneProduct(int id, ProductDtoForUpdate productDto, bool trackChanges);
        void DeleteOneProduct(int id, Product product);
    }
}
