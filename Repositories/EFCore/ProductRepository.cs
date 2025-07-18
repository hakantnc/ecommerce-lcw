using Entities.Models;
using Repositories.Contracts;
using Microsoft.EntityFrameworkCore;  // ✅ Include için gerekli
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class ProductRepository : RepositoryBase<Entities.Models.Product>, Repositories.Contracts.IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {

        }

        public void CreateOneProduct(Product product) => Create(product);

        public void DeleteOneProduct(int id, Product product)
        {
            var entity = GetOneProductsById(id, false);
            if (entity == null)
                throw new Exception($"Product with ID {id} not found");

            Delete(entity);
        }
        public IQueryable<Product> GetAllProducts(bool trackChanges) =>
            FindAll(trackChanges)
                .Include(p => p.Supplier)
                .Include(p => p.Category);
        public Product GetOneProductsById(int id, bool trackChanges) =>
            FindByCondition(p => p.Id == id, trackChanges)
            .Include(p => p.Supplier)
            .Include(p => p.Category)
            .SingleOrDefault()!;

        public void UpdateOneProduct(int id, Product product, bool trackChanges)
        {
            var entity = GetOneProductsById(id, false);
            if (entity == null)
                throw new Exception($"Product with ID {id} not found");

            entity.Name = product.Name;
            entity.Price = product.Price;
            entity.Description = product.Description;
            entity.ImageUrl = product.ImageUrl;
            entity.Stock = product.Stock;
            entity.IsActive = product.IsActive;

            Update(entity);
        }
    }
}