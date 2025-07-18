using Entities.DTO.OrderDTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IOrderDetailsService
    {
        IEnumerable<OrderDetails> GetAllOrderDetails(bool trackChanges);
        OrderDetails GetOneOrderDetailsById(int orderDetails_id, bool trackChanges);
        OrderDetails CreateOneOrderDetails(OrderDetails orderDetails);
        void UpdateOneOrderDetails(int orderDetails_id, OrderDetailsDtoForUpdate orderDetailsDto, bool trackChanges);
        void DeleteOneOrderDetails(int orderDetails_id, OrderDetails orderDetails);
        IEnumerable<OrderDetails> GetOrderDetailsByOrderId(int order_id, bool trackChanges);
    }
}
