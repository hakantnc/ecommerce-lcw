using Entities.Models;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class SubcategoryRepository : RepositoryBase<Subcategory>, ISubcategoryRepository
    {
        public SubcategoryRepository(AppDbContext context) : base(context)
        {
        }

        public void CreateSubCategory(Subcategory subCategory) => Create(subCategory);

        public void DeleteSubCategory(Subcategory subCategory) => Delete(subCategory);

        public IEnumerable<Subcategory> GetAllSubCategories(bool trackChanges) =>
            FindAll(trackChanges).ToList();

        public Subcategory? GetSubCategoryById(int id, bool trackChanges) =>
        FindByCondition(sc => sc.SubcategoryId == id, trackChanges).SingleOrDefault();

        public void UpdateSubCategory(Subcategory subCategory) => Update(subCategory);
    }
}
