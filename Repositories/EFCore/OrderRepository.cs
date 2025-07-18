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
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }

        public void CreateOneOrder(Order order)=> Create(order);

        public void DeleteOneOrder(int order_id, bool trackChanges)
        {
            var entity = GetOneOrderById(order_id, trackChanges);
            if (entity == null)
                throw new OrderNotFound(order_id);
            Delete(entity);
        }

        public IQueryable<Order> GetAllOrders(bool trackChanges) => FindAll(trackChanges)
            .Include(o => o.Cart)
            .Include(o => o.PaymentMethod);


        public Order GetOneOrderById(int order_id, bool trackChanges) => FindByCondition(o => o.Id == order_id, trackChanges)
            .Include(o => o.Cart)
            .Include(o => o.PaymentMethod)
            .FirstOrDefault() ?? throw new OrderNotFound(order_id);

        public IQueryable<Order> GetordersByCartId(int cart_id, bool trackChanges) => FindByCondition(o => o.cart_id == cart_id, trackChanges)
            .Include(o => o.Cart)
            .Include(o => o.PaymentMethod);

        public IQueryable<Order> GetOrdersByPaymentId(int payment_id, bool trackChanges) => FindByCondition(o => o.payment_id == payment_id, trackChanges)
            .Include(o => o.Cart)
            .Include(o => o.PaymentMethod);


        public void UpdateOneOrder(int order_id, Order order, bool trackChanges)
        {
            var entity = GetOneOrderById(order_id, trackChanges); 
            if (entity == null)
                throw new OrderNotFound(order_id);
            entity.cart_id = order.cart_id;
            entity.payment_id = order.payment_id;
        }
    }
}
