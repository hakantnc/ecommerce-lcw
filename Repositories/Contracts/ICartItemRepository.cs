using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface ICartItemRepository : IRepositoryBase<CartItem>
    {
        IQueryable<CartItem> GetAllCartItems(bool trackChanges);
        IQueryable<CartItem> GetCartItemsByCartId(int cart_id, bool trackChanges);
        CartItem GetOneCartItemById(int cartItem_id, bool trackChanges);
        void CreateOneCartItem(CartItem cartItem);
        void UpdateOneCartItem(int cartItem_id, CartItem cartItem);
        void DeleteOneCartItem(int cartItem_id, CartItem cartItem);
    }
}
