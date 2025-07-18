using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAllCategories(bool trackChanges);
        Category GetOneCategoryById(int category_id, bool trackChanges);
        Category CreateOneCategory(Category category);
        void UpdateOneCategory(int category_id, Category category, bool trackChanges);
        void DeleteOneCategory(int category_id, Category category);
        
        IEnumerable<Product> GetProductsByCategoryId(int category_id, bool trackChanges);
    }
}
