using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ISubcategoryService
    {
        IEnumerable<Subcategory> GetAll(bool trackChanges);
        Subcategory? GetById(int id, bool trackChanges);
        Subcategory Create(Subcategory subCategory);
        void UpdateSubCategory(Subcategory subCategory);
        void DeleteSubCategory(Subcategory subCategory);
    }
}
