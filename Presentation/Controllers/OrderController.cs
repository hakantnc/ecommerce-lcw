using Entities.DTO.OrderDTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _manager;
        private readonly ILoggerService _logger;

        public OrderController(IOrderService manager, ILoggerService logger)
        {
            _manager = manager;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult GetAllOrders(bool trackChanges = false) 
        {
            _logger.LogInfo("Fetched all orders successfully.");

            var orders = _manager.GetAllOrders(trackChanges: false);
            return Ok(orders);
        }
        [HttpGet("{order_id:int}")]
        public IActionResult GetOneOrderById(int order_id, bool trackChanges = false)
        {
            _logger.LogInfo($"Fetched order with ID {order_id} successfully.");
            var order = _manager.GetOneOrderById(order_id, trackChanges);
            return Ok(order);
        }
        [HttpPost]
        public IActionResult CreateOneOrder([FromBody] Order order)
        {
            if (order == null)
            {
                _logger.LogError("Order object sent from client is null.");
                return BadRequest("Order object is null");
            }
            var createdOrder = _manager.CreateOneOrder(order);
            _logger.LogInfo($"Created order with ID {createdOrder.Id} successfully.");
            return CreatedAtAction(nameof(GetOneOrderById), new { order_id = createdOrder.Id }, createdOrder);
        }
        [HttpPut("{order_id:int}")]
        public IActionResult UpdateOneOrder(int order_id, [FromBody] OrderDtoForUpdate orderDto)
        {
            if (orderDto == null)
            {
                _logger.LogError("Order object sent from client is null.");
                return BadRequest("Order object is null");
            }
            var existingOrder = _manager.GetOneOrderById(order_id, true);
            if (existingOrder == null)
            {
                _logger.LogError($"Order with id: {order_id} doesn't exist in the database.");
                return NotFound();
            }
            _manager.UpdateOneOrder(order_id, orderDto, true);
            _logger.LogInfo($"Updating order with ID: {order_id}");
            return NoContent();
        }
        [HttpDelete("{order_id:int}")]
        public IActionResult DeleteOneOrder(int order_id)
        {
            var order = _manager.GetOneOrderById(order_id, false);
            if (order == null)
            {
                _logger.LogError($"Order with id: {order_id} doesn't exist in the database.");
                return NotFound();
            }
            _manager.DeleteOneOrder(order_id, order);
            _logger.LogInfo($"Deleting order with ID: {order_id}");
            return NoContent();
        }
    }
}
