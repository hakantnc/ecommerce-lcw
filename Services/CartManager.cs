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
    public class CartManager : ICartService
    {
        private readonly IRepositoryManager _manager;

        public CartManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public Cart CreateOneCart(Cart cart)
        {
            _manager.CartService.CreateOneCart(cart);
            _manager.Save();
            return cart;
        }

        public void DeleteOneCart(int cart_id, Cart cart)
        {
            _manager.CartService.DeleteOneCart(cart_id, cart);
            if (cart == null)
                throw new CartNotFound(cart_id);
            _manager.Save();
           

        }

        public Cart GetActiveCartByCustomerId(int customer_id, bool trackChanges)
        {
           return _manager.CartService.GetActiveCartByCustomerId(customer_id, trackChanges);
        }

        public IEnumerable<Cart> GetAllCarts(bool trackChanges)
        {
            return _manager.CartService.GetAllCarts(trackChanges).ToList();
        }

        public Cart GetOneCartById(int cart_id, bool trackChanges)
        {
            return _manager.CartService.GetOneCartById(cart_id, trackChanges);
        }

        public void UpdateOneCart(int cart_id, CartDtoForUpdate cartDto)
        {
            var entity = _manager.CartService.GetOneCartById(cart_id, true);
            if (entity == null)
                throw new CartNotFound(cart_id);
            entity.customer_id = cartDto.CustomerId;
            entity.IsActive = cartDto.IsActive;
            _manager.CartService.UpdateOneCart(cart_id, entity);
            _manager.Save();
        }
    }
}
