using Entities.DTO.OrderDTO;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderDetailsManager : IOrderDetailsService
    {
        readonly IRepositoryManager _manager;

        public OrderDetailsManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public OrderDetails CreateOneOrderDetails(OrderDetails orderDetails)
        {
            _manager.OrderDetailsService.CreateOneOrderDetails(orderDetails);
            _manager.Save();
            return orderDetails;
        }

        public void DeleteOneOrderDetails(int orderDetails_id, OrderDetails orderDetails)
        {
            _manager.OrderDetailsService.DeleteOneOrderDetails(orderDetails_id, false);
            _manager.Save();    
        }

        public IEnumerable<OrderDetails> GetAllOrderDetails(bool trackChanges)
        {
            return _manager.OrderDetailsService.GetAllOrderDetails(trackChanges)
                .ToList();
        }

        public OrderDetails GetOneOrderDetailsById(int orderDetails_id, bool trackChanges)
        {
            return _manager.OrderDetailsService.GetOneOrderDetailsById(orderDetails_id, trackChanges);
           
        }

        public IEnumerable<OrderDetails> GetOrderDetailsByOrderId(int order_id, bool trackChanges)
        {
            return _manager.OrderDetailsService.GetOrderDetailsByOrderId(order_id, trackChanges)
                .ToList();
        }

        public void UpdateOneOrderDetails(int orderDetails_id, OrderDetailsDtoForUpdate orderDetailsDto, bool trackChanges)
        {
            var entity = _manager.OrderDetailsService.GetOneOrderDetailsById(orderDetails_id, trackChanges);
            if (entity == null)
                throw new OrderNotFound(orderDetails_id);
            entity.order_id = orderDetailsDto.OrderId;
            entity.totalAmount = orderDetailsDto.totalAmount;
            entity.isPaid = orderDetailsDto.isPaid;
            entity.status = orderDetailsDto.status;
            _manager.OrderDetailsService.UpdateOneOrderDetails(orderDetails_id, entity, trackChanges);
            _manager.Save();

        }
    }
}
