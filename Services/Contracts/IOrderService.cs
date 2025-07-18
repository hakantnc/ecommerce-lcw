using Entities.DTO.OrderDTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IOrderService
    {
        IEnumerable<Order>GetAllOrders(bool trackChanges);
        Order GetOneOrderById(int order_id, bool trackChanges);
        Order CreateOneOrder(Order order);
        void UpdateOneOrder(int order_id, OrderDtoForUpdate orderDto, bool trackChanges);
        void DeleteOneOrder(int order_id, Order order);
        IEnumerable<Order> GetOrdersByCartId(int cart_id, bool trackChanges);
        IEnumerable<Order> GetOrdersByPaymentId(int payment_id, bool trackChanges); 
    }
}
