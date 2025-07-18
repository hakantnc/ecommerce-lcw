using Entities.Exceptions;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {

        }

        public void CreateOneCategory(Category category) => Create(category);

        public void DeleteOneCategory(int category_id, Category category)
        {
            var entity = GetOneCategoryById(category_id, false);
            if (entity == null)
                throw new CategoryNotFound(category_id);
            Delete(entity);
        }

        public IQueryable<Category> GetAllCategories(bool trackChanges) => 
            FindAll(trackChanges);

        public Category GetOneCategoryById(int category_id, bool trackChanges) => 
            FindByCondition(c => c.category_id == category_id, trackChanges)
                .SingleOrDefault()!;

        public IQueryable<Product> GetProductsByCategoryId(int category_id, bool trackChanges)
        {
            return _context.Products
                .Where(p => p.category_id == category_id)
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .AsQueryable();
        }

        public void UpdateOneCategory(int category_id, Category category, bool trackChanges)
        {
            if(category == null)
                throw new ArgumentNullException(nameof(category));
            var entity = GetOneCategoryById(category_id, trackChanges);
            if (entity == null)
                throw new CategoryNotFound(category_id);
            entity.category_name = category.category_name;
            entity.category_description = category.category_description;
            Update(entity);
        }
    }
}
