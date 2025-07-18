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
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailsService _manager;
        private readonly ILogger _logger;

        public OrderDetailsController(IOrderDetailsService manager, ILogger logger)
        {
            _manager = manager;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult GetAllOrderDetails(bool trackChanges = false)
        {
            var orderDetails = _manager.GetAllOrderDetails(trackChanges);
            _logger.LogInformation("Fetching all order details");
            return Ok(orderDetails);
        }

        [HttpGet("{orderDetails_id:int}")]
        public IActionResult GetOneOrderDetailsById(int orderDetails_id, bool trackChanges = false)
        {
            var orderDetails = _manager.GetOneOrderDetailsById(orderDetails_id, trackChanges);
            if (orderDetails == null)
            {
                _logger.LogError($"Order details with id: {orderDetails_id} doesn't exist in the database.");
                return NotFound();
            }
            _logger.LogInformation($"Fetching order details with ID: {orderDetails_id}");
            return Ok(orderDetails);
        }

        [HttpGet("order/{order_id:int}")]
        public IActionResult GetOrderDetailsByOrderId(int order_id, bool trackChanges = false)
        {
            var orderDetails = _manager.GetOrderDetailsByOrderId(order_id, trackChanges);
            _logger.LogInformation($"Fetching order details for order with ID: {order_id}");
            return Ok(orderDetails);
        }

        [HttpPost]
        public IActionResult CreateOneOrderDetails([FromBody] OrderDetails orderDetails)
        {
            if (orderDetails == null)
            {
                _logger.LogError("Order details object sent from client is null.");
                return BadRequest("Order details object is null");
            }
            _manager.CreateOneOrderDetails(orderDetails);
            _logger.LogInformation("Creating new order details");
            return CreatedAtAction(nameof(GetOneOrderDetailsById), new { orderDetails_id = orderDetails.orderDetails_id }, orderDetails);
        }

        [HttpPut("{orderDetails_id:int}")]
        public IActionResult UpdateOneOrderDetails(int orderDetails_id, [FromBody] OrderDetailsDtoForUpdate orderDetailsDto)
        {
            if (orderDetailsDto == null)
            {
                _logger.LogError("Order details object sent from client is null.");
                return BadRequest("Order details object is null");
            }
            var existingOrderDetails = _manager.GetOneOrderDetailsById(orderDetails_id, true);
            if (existingOrderDetails == null)
            {
                _logger.LogError($"Order details with id: {orderDetails_id} doesn't exist in the database.");
                return NotFound();
            }
            _manager.UpdateOneOrderDetails(orderDetails_id, orderDetailsDto, true);
            _logger.LogInformation($"Updating order details with ID: {orderDetails_id}");
            return NoContent();
        }

        [HttpDelete("{orderDetails_id:int}")]
        public IActionResult DeleteOneOrderDetails(int orderDetails_id)
        {
            var orderDetails = _manager.GetOneOrderDetailsById(orderDetails_id, false);
            if (orderDetails == null)
            {
                _logger.LogError($"Order details with id: {orderDetails_id} doesn't exist in the database.");
                return NotFound();
            }
            _manager.DeleteOneOrderDetails(orderDetails_id, orderDetails);
            _logger.LogInformation($"Deleting order details with ID: {orderDetails_id}");
            return NoContent();
        }
    }
}