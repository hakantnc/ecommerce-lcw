using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Entities.Models;
using Repositories.Contracts;
using Repositories.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Exceptions;
using Entities.DTO.ProductDTO;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
        public class ProductController : ControllerBase
        {
            private readonly IServiceManager _manager;
            private readonly ILoggerService _logger;

        public ProductController(IServiceManager manager, ILoggerService logger)
            {
                _manager = manager;
                _logger = logger;
            }

            // GET: api/products ALL PRODUCTS

            [HttpGet]
            public IActionResult GetAllProducts()
            {
                _logger.LogInfo("Fetching all products");
                var products = _manager.Product.GetAllProducts(false);
    
                var response = products.Select(p => new
                {
                    id = p.Id,
                    name = p.Name,
                    description = p.Description,
                    price = p.Price,
                    stock = p.Stock,
                    imageUrl = p.ImageUrl,
                    isActive = p.IsActive,
                    category_id = p.category_id,
                    category_name = p.Category?.category_name,
                    supplier_id = p.supplier_id,
                    supplier_name = p.Supplier?.supplier_name ?? p.supplier_name
                });
    
                return Ok(response);
            }

            [HttpGet("{id}")]
            public IActionResult GetProduct(int id)
            {
                _logger.LogInfo($"Fetching product with ID: {id}");
                var product = _manager.Product.GetOneProductsById(id, false);
                if (product == null)
                {
                   throw new ProductNotFound(id);
                }
    
                // ✅ Detay response
                var response = new
                {
                    id = product.Id,
                    name = product.Name,
                    description = product.Description,
                    price = product.Price,
                    stock = product.Stock,
                    imageUrl = product.ImageUrl,
                    isActive = product.IsActive,
                    category_id = product.category_id,
                    category_name = product.Category?.category_name,
                    supplier_id = product.supplier_id,
                    supplier_name = product.Supplier?.supplier_name ?? product.supplier_name
                };
    
                return Ok(response);
}

            // POST: api/products/ CREATE PRODUCT
            [HttpPost]
            public IActionResult CreateOneProduct([FromBody] Product product)
            {
            _logger.LogInfo("Creating a new product");
            if (product == null)
            {
                return BadRequest("Product data is invalid");
            }

            if (string.IsNullOrEmpty(product.supplier_name) && product.supplier_id > 0)
            {
                var supplier = _manager.Supplier.GetOneSupplierById(product.supplier_id, false);
                if (supplier != null)
                {
                    product.supplier_name = supplier.supplier_name;
                }
            }

            product.Supplier = null;

            _manager.Product.CreateOneProduct(product);
            _manager.Save();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

            // PUT: api/products/{id} UPDATE PRODUCT
            [HttpPut("{id:int}")]
            public IActionResult UpdateOneProduct(int id, [FromBody] ProductDtoForUpdate productDto)
            {
                _logger.LogInfo($"Updating product with ID: {id}");
            if (productDto == null || productDto.Id != id)
                {
                    return BadRequest("Product data is invalid");
                }
                var existingProduct = _manager.Product.GetOneProductsById(id, false);
                if (existingProduct == null)
                {
                    return NotFound();
                }
                _manager.Product.UpdateOneProduct(id, productDto, false);
                _manager.Save();
                return NoContent();
            }

            // DELETE: api/products/{id} DELETE PRODUCT
            [HttpDelete("{id:int}")]
            public IActionResult DeleteOneProduct(int id)
            {
                _logger.LogInfo($"Deleting product with ID: {id}");

            var product = _manager.Product.GetOneProductsById(id, false);
                if (product == null)
                {
                    return NotFound();
                }
                _manager.Product.DeleteOneProduct(id, product);
                _manager.Save();
                return NoContent();
            }
        }
    
}
