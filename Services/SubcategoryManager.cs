using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SubcategoryManager : ISubcategoryService
    {
        private readonly IRepositoryManager _manager;

        public SubcategoryManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public Subcategory Create(Subcategory subCategory)
        {
            _manager.SubcategoryService.CreateSubCategory(subCategory);
            _manager.Save();
            return subCategory;
        }

        public void DeleteSubCategory(Subcategory subCategory)
        {
            _manager.SubcategoryService.DeleteSubCategory(subCategory);
            _manager.Save();
        }

        public IEnumerable<Subcategory> GetAll(bool trackChanges) => _manager.SubcategoryService.GetAllSubCategories(trackChanges);
         

        public Subcategory? GetById(int id, bool trackChanges) => _manager.SubcategoryService.GetSubCategoryById(id, trackChanges);

        public void UpdateSubCategory(Subcategory subCategory)
        {
            _manager.SubcategoryService.UpdateSubCategory(subCategory);
            _manager.Save();
        }
    }
}
