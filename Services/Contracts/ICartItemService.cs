using Entities.DTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ICartItemService
    {
        IEnumerable<CartItem> GetAllCartItems(bool trackChanges);
        IEnumerable<CartItem> GetCartItemsByCartId(int cart_id, bool trackChanges);
        CartItem GetOneCartItemById(int cartItem_id, bool trackChanges);
        CartItem CreateOneCartItem(CartItem cartItem);
        void UpdateOneCartItem(int cartItem_id, CartItemDtoForUpdate cartItemDto, bool trackChanges);
        void DeleteOneCartItem(int cartItem_id, CartItem cartItem);
    }
}
