using Entities.DTO;
using Entities.Exceptions;
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
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemService _manager;
        private readonly ILoggerService _logger;

        public CartItemController(ICartItemService manager, ILoggerService logger)
        {
            _manager = manager;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult GetAllCartItems(bool trackChanges)
        {
            var cartItems = _manager.GetAllCartItems(trackChanges);
            return Ok(cartItems);
        }

        [HttpGet("{cartItem_id:int}")]
        public IActionResult GetCartItem(int cartItem_id)
        {
            var cartItem = _manager.GetOneCartItemById(cartItem_id, false);
            if (cartItem == null)
            {
                _logger.LogError($"Cart item with id: {cartItem_id} not found.");
                return NotFound();
            }
            return Ok(cartItem);
        }
        [HttpGet("cart/{cart_id:int}")]
        public IActionResult GetCartItemsByCartId(int cart_id, bool trackChanges)
        {
            var cartItems = _manager.GetCartItemsByCartId(cart_id, trackChanges);
            if (cartItems == null || !cartItems.Any())
            {
                _logger.LogError($"No cart items found for cart id: {cart_id}.");
                return NotFound();
            }
            return Ok(cartItems);
        }
        [HttpPost]
        public IActionResult CreateCartItem([FromBody] CartItem cartItem)
        {
            if (cartItem == null)
            {
                _logger.LogError("Cart item is null.");
                return BadRequest("Cart item cannot be null.");
            }
            var createdCartItem = _manager.CreateOneCartItem(cartItem);
            return CreatedAtAction(nameof(GetCartItem), new { id = createdCartItem.cartItem_id }, createdCartItem);
        }
        [HttpPut("{cartItem_id:int}")]
        public IActionResult UpdateCartItem(int cartItem_id, [FromBody] CartItemDtoForUpdate cartItemDto, bool trackChanges)
        {
            if(cartItemDto == null)
            {
                _logger.LogError("Cart item DTO is null.");
                return BadRequest("Cart item DTO cannot be null.");
            }
            var entity = _manager.GetOneCartItemById(cartItem_id, trackChanges);
            if (entity == null)
            {
                _logger.LogError($"Cart item with id: {cartItem_id} not found.");
                return NotFound();
            }
            entity.cart_id = cartItemDto.CartId;
            entity.product_id = cartItemDto.ProductId;
            entity.quantity = cartItemDto.Quantity;
            entity.addedDate = cartItemDto.AddedDate;
            _manager.UpdateOneCartItem(cartItem_id, cartItemDto, trackChanges);
            return NoContent();
        }

    }
}
