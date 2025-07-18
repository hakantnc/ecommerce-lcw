using Entities.DTO.OrderDTO;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderManager : IOrderService
    {
        private readonly IRepositoryManager _manager;

        public OrderManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public Order CreateOneOrder(Order order)
        {
            _manager.OrderService.CreateOneOrder(order);
            _manager.Save();
            return order;
        }

        public void DeleteOneOrder(int order_id, Order order)
        {
            _manager.OrderService.DeleteOneOrder(order_id, false);
            _manager.Save();

        }

        public IEnumerable<Order> GetAllOrders(bool trackChanges)
        {
            return _manager.OrderService.GetAllOrders(trackChanges).ToList();
        }

        public Order GetOneOrderById(int order_id, bool trackChanges)
        {
           return _manager.OrderService.GetOneOrderById(order_id, trackChanges);
        }

        public IEnumerable<Order> GetOrdersByCartId(int cart_id, bool trackChanges)
        {
            return _manager.OrderService.GetordersByCartId(cart_id, trackChanges).ToList();
        }

        public IEnumerable<Order> GetOrdersByPaymentId(int payment_id, bool trackChanges)
        {
            return _manager.OrderService.GetOrdersByPaymentId(payment_id, trackChanges).ToList();
        }

        public void UpdateOneOrder(int order_id, OrderDtoForUpdate orderDto, bool trackChanges)
        {
            var entity = _manager.OrderService.GetOneOrderById(order_id, trackChanges);
            if (entity == null)
                throw new OrderNotFound(order_id);
            entity.cart_id = orderDto.CartId;
            entity.payment_id = orderDto.PaymentId;
            _manager.OrderService.UpdateOneOrder(order_id, entity, trackChanges);
            _manager.Save();
        }
    }
}
