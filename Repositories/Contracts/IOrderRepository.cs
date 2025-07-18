using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public  interface IOrderRepository : IRepositoryBase<Order>
    {
        IQueryable<Order> GetAllOrders(bool trackChanges);
        Order GetOneOrderById(int order_id, bool trackChanges);
        void CreateOneOrder(Order order);
        void UpdateOneOrder(int order_id, Order order, bool trackChanges);
        void DeleteOneOrder(int order_id, bool trackChanges);
        IQueryable<Order> GetordersByCartId(int cart_id, bool trackChanges);
        IQueryable<Order> GetOrdersByPaymentId(int payment_id, bool trackChanges);
    }
}
