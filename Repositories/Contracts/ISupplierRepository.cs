using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
namespace Repositories.Contracts
{
    public interface ISupplierRepository : IRepositoryBase<Supplier>
    {
        IQueryable<Supplier> GetAllSuppliers(bool trackChanges);
        public Supplier GetOneSupplierById(int supplier_id, bool trackChanges);
        void CreateOneSupplier(Supplier supplier);
        void UpdateOneSupplier(int supplier_id, Supplier supplier, bool trackChanges);
        void DeleteOneSupplier(int supplier_id, Supplier supplier);

        IQueryable<Product> GetProductsBySupplierId(int supplier_id, bool trackChanges);
        Supplier? GetByEmail(string email);
    }
}
