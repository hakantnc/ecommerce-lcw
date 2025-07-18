using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
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
    public class CartController : ControllerBase
    {
        private readonly ICartService _manager;
        private readonly ILoggerService _logger;

        public CartController(ICartService manager,ILoggerService logger)
        {
            _manager = manager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllCarts(bool trackChanges)
        {
            var carts = _manager.GetAllCarts(trackChanges);
            _logger.LogInfo("Returned all carts from database.");
            return Ok(carts);
        }
        [HttpGet("{cart_id:int}")]
        public IActionResult GetOneCartById(int cart_id, bool trackChanges)
        {
            var cart = _manager.GetOneCartById(cart_id, trackChanges);
            if (cart == null)
            {
                _logger.LogError($"Cart with id: {cart_id} doesn't exist in the database.");
                return NotFound();
            }
            _logger.LogInfo($"Returned cart with id: {cart_id} from database.");
            return Ok(cart);
        }
        [HttpGet("active/{customer_id:int}")]
        public IActionResult GetActiveCartByCustomerId(int customer_id, bool trackChanges)
        {
            var cart = _manager.GetActiveCartByCustomerId(customer_id, trackChanges);
            if (cart == null)
            {
                _logger.LogError($"Active cart for customer with id: {customer_id} doesn't exist in the database.");
                return NotFound();
            }
            _logger.LogInfo($"Returned active cart for customer with id: {customer_id} from database.");
            return Ok(cart);
        }

        [HttpPost]
          public IActionResult CreateOneCart([FromBody] Cart cart)
        {
            if (cart == null)
            {
                _logger.LogError("Cart object sent from client is null.");
                return BadRequest("Cart object is null");
            }
            _manager.CreateOneCart(cart);
            _logger.LogInfo("Created a new cart in the database.");
            return CreatedAtAction(nameof(GetOneCartById), new { cart_id = cart.Id }, cart);
        }

        [HttpPut("{cart_id:int}")]
        public IActionResult UpdateOneCart(int cart_id, [FromBody] CartDtoForUpdate cartDto)
        {
            if (cartDto == null)
            {
                _logger.LogError("Cart object sent from client is null.");
                return BadRequest("Cart object is null");
            }
            var existingCart = _manager.GetOneCartById(cart_id, true);
            if (existingCart == null)
            {
                _logger.LogError($"Cart with id: {cart_id} doesn't exist in the database.");
                return NotFound();
            }
            _manager.UpdateOneCart(cart_id, cartDto);
            _logger.LogInfo($"Updated cart with id: {cart_id} in the database.");
            return NoContent();
        }

        [HttpDelete("{cart_id:int}")]
        public IActionResult DeleteOneCart(int cart_id)
        {
            var cart = _manager.GetOneCartById(cart_id, false);
            if (cart == null)
            {
                _logger.LogError($"Cart with id: {cart_id} doesn't exist in the database.");
                return NotFound();
            }
            _manager.DeleteOneCart(cart_id, cart);
            _logger.LogInfo($"Deleted cart with id: {cart_id} from the database.");
            return NoContent();
        }

    }
}
