using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IProductRepository ProductService { get; } // Ensure IProductRepository is public  
        ISupplierRepository SupplierService { get; } // Ensure ISupplierRepository is public

        ICategoryRepository CategoryService { get; } // Ensure ICategoryRepository is public
        ICustomerRepository CustomerService { get; } // Ensure ICustomerRepository is public

        IPaymentRepository PaymentService { get; } 
        ICartRepository CartService { get; }
        ICartItemRepository CartItemService { get; }
        void Save();
    }
}
