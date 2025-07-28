using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubcategoryController : ControllerBase
    {
        private readonly IServiceManager _manager;
        private readonly ILoggerService _logger;

        public SubcategoryController(IServiceManager manager, ILoggerService logger)
        {
            _manager = manager;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult GetAllSubcategories(bool trackChanges = false)
        {
            _logger.LogInfo("Fetching all subcategories");
            var subcategories = _manager.Subcategory.GetAll(trackChanges);
            return Ok(subcategories);
        }
        [HttpGet("{subcategory_id}")]
        public IActionResult GetSubcategoryById(int subcategory_id, bool trackChanges = false)
        {
            _logger.LogInfo($"Fetching subcategory with ID: {subcategory_id}");
            var subcategory = _manager.Subcategory.GetById(subcategory_id, trackChanges);
            if (subcategory == null)
            {
                throw new SubcategoryNotFound(subcategory_id);
            }
            return Ok(subcategory);
        }
        [HttpGet("by-category/{category_id}")]
        public IActionResult GetSubcategoriesByCategoryId(int category_id, bool trackChanges = false)
        {
            _logger.LogInfo($"Fetching subcategories for category with ID: {category_id}");
            var subcategories = _manager.Subcategory.GetAll(trackChanges)
                .Where(s => s.category_id == category_id)
                .ToList();
            return Ok(subcategories);
        }
        [HttpPost]
        public IActionResult CreateOneSubcategory([FromBody] Entities.Models.Subcategory subcategory)
        {
            _logger.LogInfo("Creating a new subcategory");
            if (subcategory == null)
            {
                return BadRequest("Subcategory cannot be null");
            }
            _manager.Subcategory.Create(subcategory);
            _manager.Save();
            return CreatedAtAction(nameof(GetSubcategoryById), new { subcategory_id = subcategory.sub_id }, subcategory);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateSubCategory(int id, [FromBody] Subcategory subCategory)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingSubCategory = _manager.Subcategory.GetById(id, true);
            if (existingSubCategory == null)
                throw new SubcategoryNotFound(id);

            existingSubCategory.sub_name = subCategory.sub_name;
            existingSubCategory.category_id = subCategory.category_id;

            _manager.Subcategory.UpdateSubCategory(existingSubCategory);
            _manager.Save();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSubCategory(int id)
        {
            var subcategory = _manager.Subcategory.GetById(id, false);
            if (subcategory == null)
                throw new SubcategoryNotFound(id);

            _manager.Subcategory.DeleteSubCategory(subcategory);
            _manager.Save();
            return NoContent();
        }
    }
}
