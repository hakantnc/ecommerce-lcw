using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IServiceManager
    {
        public IProductService Product { get; }
        public ISupplierService Supplier { get; }

        public ICategoryService Category { get; }

        public ICustomerService Customer { get; }
        public IPaymentService Payment { get; }
        public IOrderService Order { get; }
        public IOrderDetailsService OrderDetails { get; }
        public ICartService Cart { get; }
        public ICartItemService CartItem { get; }
        public ISubcategoryService Subcategory { get; }

        void Save();
    }
}
