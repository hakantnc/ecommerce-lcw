using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IOrderDetailsRepository : IRepositoryBase<OrderDetails>
    {
        IQueryable<OrderDetails> GetAllOrderDetails(bool trackChanges);
        OrderDetails GetOneOrderDetailsById(int orderDetails_id, bool trackChanges);
        void CreateOneOrderDetails(OrderDetails orderDetails);
        void UpdateOneOrderDetails(int orderDetails_id, OrderDetails orderDetails, bool trackChanges);
        void DeleteOneOrderDetails(int orderDetails_id, bool trackChanges);
        IQueryable<OrderDetails> GetOrderDetailsByOrderId(int order_id, bool trackChanges);
    }
}
