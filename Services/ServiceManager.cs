using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private IRepositoryManager _repositoryManager;
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<ISupplierService> _supplierService;
        private readonly Lazy<ICategoryService> _categoryService;
        private readonly Lazy<ICustomerService> _customerService;
        private readonly Lazy<IPaymentService> _paymentService;
        private readonly Lazy<ICartService> _cartService;
        private readonly Lazy<ICartItemService> _cartItemService;   
        private readonly Lazy<IOrderDetailsService> _orderDetailsService;   
        private readonly Lazy<IOrderService> _orderService;
        private readonly Lazy<ISubcategoryService> _subcategoryService;
        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
            _productService = new Lazy<IProductService>(() => new ProductManager(repositoryManager));  
            _supplierService = new Lazy<ISupplierService>(() => new SupplierManager(repositoryManager)) ;
            _categoryService = new Lazy<ICategoryService>(() => new CategoryManager(repositoryManager));
            _customerService = new Lazy<ICustomerService>(() => new CustomerManager(repositoryManager));
            _paymentService = new Lazy<IPaymentService>(() => new PaymentManager(repositoryManager));
            _cartService = new Lazy<ICartService>(() => new CartManager(repositoryManager));
           _cartItemService = new Lazy<ICartItemService>(() => new CartItemManager(repositoryManager)); 
            _orderService = new Lazy<IOrderService>(() => new OrderManager(repositoryManager));
            _orderDetailsService = new Lazy<IOrderDetailsService>(() => new OrderDetailsManager(repositoryManager));
            _subcategoryService = new Lazy<ISubcategoryService>(() => new SubcategoryManager(repositoryManager));
        }
        public IProductService Product => _productService.Value;
        public ISupplierService Supplier => _supplierService.Value;
        public ICategoryService Category => _categoryService.Value;
        public ICustomerService Customer => _customerService.Value;
        public IPaymentService Payment => _paymentService.Value;
        public ICartService Cart => _cartService.Value;
        public ICartItemService CartItem => _cartItemService.Value;
        public IOrderService Order => _orderService.Value;
        public IOrderDetailsService OrderDetails => _orderDetailsService.Value;
        public ISubcategoryService Subcategory => _subcategoryService.Value;
        public void Save()
        {
            _repositoryManager.Save();
        }

    }
}
