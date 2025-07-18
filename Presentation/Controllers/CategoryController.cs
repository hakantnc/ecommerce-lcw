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
    public class CategoryController : ControllerBase
    {
        private readonly IServiceManager _manager;
        private readonly ILoggerService _logger;
        public CategoryController(IServiceManager manager, ILoggerService logger)
        {
            _manager = manager;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult GetAllCategories(bool trackChanges = false)
        {
            _logger.LogInfo("Fetching all categories");
            var categories = _manager.Category.GetAllCategories(trackChanges);
            return Ok(categories);
        }
        [HttpGet("{category_id}")]
        public IActionResult GetCategoryById(int category_id, bool trackChanges = false)
        {
            _logger.LogInfo($"Fetching category with ID: {category_id}");
            var category = _manager.Category.GetOneCategoryById(category_id, trackChanges);
            if (category == null)
            {
                throw new CategoryNotFound(category_id);
            }
            return Ok(category);
        }
        [HttpGet("{category_id}/products")]
        public IActionResult GetProductsByCategoryId(int category_id, bool trackChanges = false)
        {
            _logger.LogInfo($"Fetching products for category with ID: {category_id}");
            var products = _manager.Category.GetProductsByCategoryId(category_id, trackChanges);
            return Ok(products);
        }
        [HttpPost]
        public IActionResult CreateOneCategory([FromBody] Entities.Models.Category category)
        {
            _logger.LogInfo("Creating a new category");
            if (category == null)
            {
                return BadRequest("Category cannot be null");
            }
            _manager.Category.CreateOneCategory(category);
            _manager.Save();
            return CreatedAtAction(nameof(GetCategoryById), new { category_id = category.category_id }, category);
        }
        [HttpPut("{category_id}")]
        public IActionResult UpdateOneCategory(int category_id, [FromBody] Category category, bool trackChanges = true)
        {
            _logger.LogInfo($"Updating category with ID: {category_id}");
            if (category == null)
            {
                return BadRequest("Category cannot be null");
            }
            var existingCategory = _manager.Category.GetOneCategoryById(category_id, trackChanges);
            if (existingCategory == null)
            {
                throw new CategoryNotFound(category_id);
            }
            _manager.Category.UpdateOneCategory(category_id, category, trackChanges);
            _manager.Save();
            return NoContent();
        }
        [HttpDelete("{category_id}")]
        public IActionResult DeleteOneCategory(int category_id, bool trackChanges = false)
        {
            _logger.LogInfo($"Deleting category with ID: {category_id}");
            var existingCategory = _manager.Category.GetOneCategoryById(category_id, trackChanges);
            if (existingCategory == null)
            {
                throw new CategoryNotFound(category_id);
            }
            _manager.Category.DeleteOneCategory(category_id, existingCategory);
            _manager.Save();
            return NoContent();


        }
    }
}
