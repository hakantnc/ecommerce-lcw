using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly AppDbContext _context;
        private readonly Lazy<IProductRepository> _productRepository;
        private readonly Lazy<ISupplierRepository> _supplierRepository;
        private readonly Lazy<ICategoryRepository> _categoryRepository;
        private readonly Lazy<ICustomerRepository> _customerRepository;
        private readonly Lazy<IPaymentRepository> _paymentRepository;
        private readonly Lazy<ICartRepository> _cartRepository;
        private readonly Lazy<ICartItemRepository> _cartItemRepository;
        private readonly Lazy<IOrderRepository> _orderRepository;
        private readonly Lazy<IOrderDetailsRepository> _orderDetailsRepository;
        private readonly Lazy<ISubcategoryRepository> _subcategoryRepository;
        public RepositoryManager(AppDbContext context)
        {
            _context = context;
            _productRepository = new Lazy<IProductRepository>(() => new ProductRepository(_context));
            _supplierRepository = new Lazy<ISupplierRepository>(() => new SupplierRepository(_context));
            _categoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(_context));
            _customerRepository = new Lazy<ICustomerRepository>(() => new CustomerRepository(_context));
            _paymentRepository = new Lazy<IPaymentRepository>(() => new PaymentRepository(_context));
            _cartRepository = new Lazy<ICartRepository>(() => new CartRepository(_context));
            _cartItemRepository = new Lazy<ICartItemRepository>(() => new CartItemRepository(_context));
            _orderDetailsRepository = new Lazy<IOrderDetailsRepository>(() => new OrderDetailsRepository(_context));
            _orderRepository = new Lazy<IOrderRepository>(() => new OrderRepository(_context));
            _subcategoryRepository = new Lazy<ISubcategoryRepository>(() => new SubcategoryRepository(_context));
        }
        public IProductRepository ProductService => _productRepository.Value;

        public ISupplierRepository SupplierService => _supplierRepository.Value;
        public ICategoryRepository CategoryService => _categoryRepository.Value;
        public ICustomerRepository CustomerService => _customerRepository.Value;
        public IPaymentRepository PaymentService => _paymentRepository.Value;
        public ICartRepository CartService => _cartRepository.Value;
        public ICartItemRepository CartItemService => _cartItemRepository.Value;

        public IOrderRepository OrderService => _orderRepository.Value;
        public IOrderDetailsRepository OrderDetailsService => _orderDetailsRepository.Value;
        public ISubcategoryRepository SubcategoryService => _subcategoryRepository.Value;
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
