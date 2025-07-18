using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        IQueryable<Category> GetAllCategories(bool trackChanges);
        Category GetOneCategoryById(int category_id, bool trackChanges);
        void CreateOneCategory(Category category);
        void UpdateOneCategory(int category_id, Category category, bool trackChanges);
        void DeleteOneCategory(int category_id, Category category);
        IQueryable<Product> GetProductsByCategoryId(int category_id, bool trackChanges);
    }
}
