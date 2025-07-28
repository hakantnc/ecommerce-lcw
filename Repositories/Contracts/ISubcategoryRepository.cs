using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface ISubcategoryRepository
    {
        IEnumerable<Subcategory> GetAllSubCategories(bool trackChanges);
        Subcategory? GetSubCategoryById(int id, bool trackChanges);
        void CreateSubCategory(Subcategory subCategory);
        void UpdateSubCategory(Subcategory subCategory);
        void DeleteSubCategory(Subcategory subCategory);
    }

}
