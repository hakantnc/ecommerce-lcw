using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public abstract class NotFound : Exception
    {
        public NotFound(string message) : base(message)
        {

        }
    }
    public sealed class ProductNotFound : NotFound
    {
        public ProductNotFound(int id) : base($"The book with id:{id} could not found.")
        {

        }


    }
    public sealed class SupplierNotFound : NotFound
    {
        public SupplierNotFound(int id) : base($"The supplier with id:{id} could not found.")
        {
        }
    }

    public sealed class CategoryNotFound : NotFound
    {
        public CategoryNotFound(int id) : base($"The category with id:{id} could not found.")
        {
        }
    }

    public sealed class CustomerNotFound : NotFound
    {
        public CustomerNotFound(int id) : base($"The customer with id:{id} could not found.")
        {
        }
    }
    public sealed class PaymentNotFound : NotFound
    {
        public PaymentNotFound(int id) : base($"The payment method with id:{id} could not found.")
        {
        }
    }
    public sealed class CartNotFound : NotFound
    {
        public CartNotFound(int id) : base($"The cart with id:{id} could not found.")
        {
        }
    }
    public sealed class OrderNotFound : NotFound
    {
        public OrderNotFound(int id) : base($"The order with id:{id} could not found.")
        { }
    }
    public sealed class SubcategoryNotFound : NotFound
    {
        public SubcategoryNotFound(int id) : base($"The subcategory with id:{id} could not found.")
        {
        }
    }
}
