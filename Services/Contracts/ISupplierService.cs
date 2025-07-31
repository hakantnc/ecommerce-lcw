using Entities.DTO.SupplierDTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ISupplierService
    {
        IEnumerable<Supplier> GetAllSuppliers(bool trackChanges);
        Supplier GetOneSupplierById(int supplier_id, bool trackChanges); 
        Supplier CreateOneSupplier(Supplier supplier);
        void UpdateOneSupplier(int supplier_id, SupplierDtoForUpdate supplierDto, bool trackChanges);
        void DeleteOneSupplier(int supplier_id, Supplier supplier);
        IEnumerable<Product> GetProductsBySupplierId(int supplier_id, bool trackChanges);
        Supplier? GetByRefreshToken(string refreshToken);

    }
}
