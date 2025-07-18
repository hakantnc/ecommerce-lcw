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
    public class SupplierRepository : RepositoryBase<Supplier>, ISupplierRepository
    {
        public SupplierRepository(AppDbContext context) : base(context)
        {
        }

        public void CreateOneSupplier(Supplier supplier) => Create(supplier);

        public void DeleteOneSupplier(int supplier_id, Supplier supplier)
        {
            var entity = GetOneSupplierById(supplier_id, false);
            if (entity == null)
                throw new SupplierNotFound(supplier_id);

            Delete(entity);
        }

        public IQueryable<Supplier> GetAllSuppliers(bool trackChanges) => 
            FindAll(trackChanges);

        public Supplier GetOneSupplierById(int supplier_id, bool trackChanges) => 
            FindByCondition(s => s.supplier_id == supplier_id, trackChanges)
                .SingleOrDefault()!;

        public IQueryable<Product> GetProductsBySupplierId(int supplier_id, bool trackChanges)
        {
            return _context.Products
                .Where(p => p.supplier_id == supplier_id)
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .AsQueryable();
        }

        public void UpdateOneSupplier(int supplier_id, Supplier supplier, bool trackChanges)
        {
            if (supplier == null)
                throw new ArgumentNullException(nameof(supplier));

            var entity = GetOneSupplierById(supplier_id, trackChanges);
            if (entity == null)
                throw new SupplierNotFound(supplier_id);

            entity.supplier_name = supplier.supplier_name;
            entity.supplier_email = supplier.supplier_email;
            entity.supplier_phone = supplier.supplier_phone;
            entity.supplier_address = supplier.supplier_address;
            entity.supplier_city = supplier.supplier_city;
            entity.supplier_country = supplier.supplier_country;

            Update(entity);
        }
    }
}
