using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.EFCore;
using Entities.Exceptions;
using Entities.DTO.SupplierDTO;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly IServiceManager _manager;
        private readonly ILoggerService _logger;
        public SupplierController(IServiceManager manager, ILoggerService logger)
        {
            _manager = manager;
            _logger = logger;
        }


        [HttpGet]
        public IActionResult GetAllSuppliers(bool trackChanges = false)
        {
            _logger.LogInfo("Fetching all suppliers");
            var suppliers = _manager.Supplier.GetAllSuppliers(trackChanges);
            return Ok(suppliers);
        }
        [HttpGet("{supplier_id}")]
        public IActionResult GetSupplierById(int supplier_id, bool trackChanges = false)
        {
            _logger.LogInfo($"Fetching supplier with ID: {supplier_id}");
            var supplier = _manager.Supplier.GetOneSupplierById(supplier_id, trackChanges);
            if (supplier == null)
            {
                throw new SupplierNotFound(supplier_id);
            }
            return Ok(supplier);
        }
        [HttpGet("{supplier_id}/products")]
        public IActionResult GetProductsBySupplierId(int supplier_id, bool trackChanges = false)
        {
            _logger.LogInfo($"Fetching products for supplier with ID: {supplier_id}");
            var products = _manager.Supplier.GetProductsBySupplierId(supplier_id, trackChanges);
            return Ok(products);

        }
        [HttpPost]
        public IActionResult CreateOneSupplier([FromBody] Supplier supplier)
        {
            _logger.LogInfo("Creating a new supplier");
            if (supplier == null)
            {
                return BadRequest("Supplier data is invalid");
            }
            var createdSupplier = _manager.Supplier.CreateOneSupplier(supplier);
            return CreatedAtAction(nameof(GetSupplierById), new { supplier_id = createdSupplier.supplier_id }, createdSupplier);
        }
        [HttpPut("{supplier_id}")]
        public IActionResult UpdateOneSupplier(int supplier_id, [FromBody] SupplierDtoForUpdate supplierDto)
        {
            _logger.LogInfo($"Updating supplier with ID: {supplier_id}");
            if (supplierDto == null)
            {
                return BadRequest("Supplier data is invalid");
            }
            var existingSupplier = _manager.Supplier.GetOneSupplierById(supplier_id, false);
            if (existingSupplier == null)
            {
                throw new SupplierNotFound(supplier_id);
            }
            _manager.Supplier.UpdateOneSupplier(supplier_id, supplierDto, false);
            _manager.Save();
            return NoContent();
        }
        [HttpDelete("{supplier_id}")]
        public IActionResult DeleteOneSupplier(int supplier_id)
        {
            _logger.LogInfo($"Deleting supplier with ID: {supplier_id}");
            var supplier = _manager.Supplier.GetOneSupplierById(supplier_id, false);
            if (supplier == null)
            {
                throw new SupplierNotFound(supplier_id);
            }
            _manager.Supplier.DeleteOneSupplier(supplier_id, supplier);
            _manager.Save();
            return NoContent();

        }
    }
}
