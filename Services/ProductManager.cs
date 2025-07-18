using Entities.DTO.ProductDTO;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductManager : IProductService
    {
        private readonly IRepositoryManager _manager;

        public ProductManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public Product CreateOneProduct(Product product)
        {
            // supplier_id'ye göre supplier_name'i otomatik doldur
            if (product.supplier_id > 0 && string.IsNullOrEmpty(product.supplier_name))
            {
                var supplier = _manager.SupplierService.GetOneSupplierById(product.supplier_id, false);
                if (supplier != null)
                {
                    product.supplier_name = supplier.supplier_name;
                }
            }

            _manager.ProductService.CreateOneProduct(product);
            _manager.Save();
            return product;
        }

        public void DeleteOneProduct(int id, Product product)
        {
            var entity = _manager.ProductService.GetOneProductsById(id, false);
            if (entity == null)
                throw new Exception($"Product with ID {id} not found");
            _manager.ProductService.DeleteOneProduct(id, product);
            _manager.Save();
        }
     
        public IEnumerable<Product> GetAllProducts(bool trackChanges)
        {
            return _manager.ProductService.GetAllProducts(trackChanges).ToList();
        }

        public Product GetOneProductsById(int id, bool trackChanges)
        {
            return _manager.ProductService.GetOneProductsById(id, trackChanges);
        }

        public void UpdateOneProduct(int id, ProductDtoForUpdate productDto, bool trackChanges)
        {
            var entity = _manager.ProductService.GetOneProductsById(id, trackChanges);
            if (entity == null)
                throw new Exception($"Product with ID {id} not found");

            entity.Name = productDto.Name;
            entity.Price = productDto.Price;
            entity.Description = productDto.Description;
            entity.ImageUrl = productDto.ImageUrl;
            entity.Stock = productDto.Stock;
            entity.IsActive = productDto.IsActive;
            
            // supplier_id güncelleniyorsa supplier_name'i de güncelle
            if (entity.supplier_id != productDto.SupplierId)
            {
                entity.supplier_id = productDto.SupplierId;
                var supplier = _manager.SupplierService.GetOneSupplierById(productDto.SupplierId, false);
                if (supplier != null)
                {
                    entity.supplier_name = supplier.supplier_name;
                }
            }

            _manager.ProductService.UpdateOneProduct(id, entity, trackChanges);
            _manager.Save();
        }
    }
}