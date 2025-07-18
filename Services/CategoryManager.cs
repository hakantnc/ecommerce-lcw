using Entities.Exceptions;
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
    public class CategoryManager : ICategoryService
    {
        private readonly IRepositoryManager _manager;

        public CategoryManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public Category CreateOneCategory(Category category)
        {
            _manager.CategoryService.CreateOneCategory(category);
            _manager.Save();
            return category;
        }

        public void DeleteOneCategory(int category_id, Category category)
        {
           var entity = _manager.CategoryService.GetOneCategoryById(category_id, false);
            if (entity == null)
                throw new CategoryNotFound(category_id);
            _manager.CategoryService.DeleteOneCategory(category_id, category);
            _manager.Save();
        }

        public IEnumerable<Category> GetAllCategories(bool trackChanges)
        {
           return _manager.CategoryService.GetAllCategories(trackChanges);
        }

        public Category GetOneCategoryById(int category_id, bool trackChanges)
        {
            return _manager.CategoryService.GetOneCategoryById(category_id, trackChanges);
        }

        public IEnumerable<Product> GetProductsByCategoryId(int category_id, bool trackChanges)
        {
            return _manager.CategoryService.GetProductsByCategoryId(category_id, trackChanges).ToList();
        }

        public void UpdateOneCategory(int category_id, Category category, bool trackChanges)
        {
           var entity = _manager.CategoryService.GetOneCategoryById(category_id, trackChanges);
            if (entity == null)
                throw new CategoryNotFound(category_id);
            entity.category_name = category.category_name;
            entity.category_description = category.category_description;
            _manager.CategoryService.UpdateOneCategory(category_id, entity, trackChanges);
            _manager.Save();
        }
    }
}
