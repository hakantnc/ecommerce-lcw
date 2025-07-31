using Entities.DTO.SupplierDTO;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SupplierManager : ISupplierService
    {
        private readonly IRepositoryManager _manager;

        public SupplierManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public Supplier CreateOneSupplier(Supplier supplier)
        {
            _manager.SupplierService.CreateOneSupplier(supplier);
            _manager.Save();
            return supplier;
        }

        public void DeleteOneSupplier(int supplier_id, Supplier supplier)
        {
            var entity = _manager.SupplierService.GetOneSupplierById(supplier_id, false);
            if (entity == null)
                throw new SupplierNotFound(supplier_id);
            _manager.SupplierService.DeleteOneSupplier(supplier_id, supplier);
            _manager.Save();
        }

        public IEnumerable<Supplier> GetAllSuppliers(bool trackChanges)
        {
            return _manager.SupplierService.GetAllSuppliers(trackChanges).ToList();
        }

        public Supplier GetOneSupplierById(int supplier_id, bool trackChanges)
        {
            return _manager.SupplierService.GetOneSupplierById(supplier_id, trackChanges);
        }

        public IEnumerable<Product> GetProductsBySupplierId(int supplier_id, bool trackChanges)
        {
            return _manager.SupplierService.GetProductsBySupplierId(supplier_id, trackChanges).ToList();
        }

        public void UpdateOneSupplier(int supplier_id, SupplierDtoForUpdate supplierDto, bool trackChanges)
        {
            var entity = _manager.SupplierService.GetOneSupplierById(supplier_id, trackChanges);
            if (entity == null)
                throw new SupplierNotFound(supplier_id);

            entity.supplier_name = supplierDto.supplier_name;
            entity.supplier_email = supplierDto.supplier_email;
            entity.supplier_phone = supplierDto.supplier_phone;
            entity.supplier_address = supplierDto.supplier_address;
            entity.supplier_city = supplierDto.supplier_city;
            entity.supplier_country = supplierDto.supplier_country;

            _manager.SupplierService.UpdateOneSupplier(supplier_id, entity, trackChanges);
            _manager.Save();
        }

        public Supplier? GetByRefreshToken(string refreshToken)
        {
            return _manager.SupplierService.GetByRefreshToken(refreshToken);
        }

    }
}
