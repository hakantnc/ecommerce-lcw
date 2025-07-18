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
    public class OrderDetailsRepository : RepositoryBase<OrderDetails>, IOrderDetailsRepository
    {
        public OrderDetailsRepository(AppDbContext context) : base(context)
        {
        }

        public void CreateOneOrderDetails(OrderDetails orderDetails) => Create(orderDetails);

        public void DeleteOneOrderDetails(int orderDetails_id, bool trackChanges)
        {
            var entity = GetOneOrderDetailsById(orderDetails_id, trackChanges); 
            if (entity == null)
                throw new OrderNotFound(orderDetails_id);
            Delete(entity);

        }

        public IQueryable<OrderDetails> GetAllOrderDetails(bool trackChanges) => FindAll(trackChanges);
         
        public OrderDetails GetOneOrderDetailsById(int orderDetails_id, bool trackChanges) => FindByCondition(od => od.orderDetails_id == orderDetails_id, trackChanges)
            .SingleOrDefault() ?? throw new OrderNotFound(orderDetails_id);

        public IQueryable<OrderDetails> GetOrderDetailsByOrderId(int order_id, bool trackChanges) => FindByCondition(od => od.order_id == order_id, trackChanges)
            .Include(od => od.Order)
            .Include(od => od.Product);

        public void UpdateOneOrderDetails(int orderDetails_id, OrderDetails orderDetails, bool trackChanges)
        {
          var entity = GetOneOrderDetailsById(orderDetails_id, trackChanges);
            if (entity == null)
                throw new OrderNotFound(orderDetails_id);

            entity.order_id = orderDetails.order_id;
            entity.totalAmount = orderDetails.totalAmount;
            entity.isPaid = orderDetails.isPaid;
            entity.status = orderDetails.status;
            Update(entity);
        }
    }
}
