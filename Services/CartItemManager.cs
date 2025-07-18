using Entities.DTO;
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
    public class CartItemManager : ICartItemService
    {
        private readonly IRepositoryManager _manager;

        public CartItemManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public CartItem CreateOneCartItem(CartItem cartItem)
        {
            _manager.CartItemService.CreateOneCartItem(cartItem);
            _manager.Save();

            return cartItem;
        }

        public void DeleteOneCartItem(int cartItem_id, CartItem cartItem)
        {
            _manager.CartItemService.DeleteOneCartItem(cartItem_id, cartItem);
            _manager.Save();
            if (cartItem == null)
                throw new CartNotFound(cartItem_id);
        }

        public IEnumerable<CartItem> GetAllCartItems(bool trackChanges)
        {
            return _manager.CartItemService.GetAllCartItems(trackChanges).ToList();
        }

        public IEnumerable<CartItem> GetCartItemsByCartId(int cart_id, bool trackChanges)
        {
            return _manager.CartItemService.GetCartItemsByCartId(cart_id, trackChanges).ToList() 
                ?? throw new CartNotFound(cart_id);
        }

        public CartItem GetOneCartItemById(int cartItem_id, bool trackChanges)
        {
            return _manager.CartItemService.GetOneCartItemById(cartItem_id, trackChanges)
                ?? throw new CartNotFound(cartItem_id);
        }

        public void UpdateOneCartItem(int cartItem_id, CartItemDtoForUpdate cartItemDto, bool trackChanges)
        {
            var entity = _manager.CartItemService.GetOneCartItemById(cartItem_id, trackChanges);
            if (entity == null)
                throw new CartNotFound(cartItem_id); 
            entity.cart_id = cartItemDto.CartId; 
            entity.product_id = cartItemDto.ProductId;
            entity.quantity = cartItemDto.Quantity;
            entity.addedDate = cartItemDto.AddedDate;
            _manager.CartItemService.UpdateOneCartItem(cartItem_id, entity);
            _manager.Save();

        }
    }
}
