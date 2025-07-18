using Entities.Exceptions;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class CartItemRepository : RepositoryBase<CartItem>, ICartItemRepository
    {
        public CartItemRepository(AppDbContext context) : base(context)
        {
        }

        public void CreateOneCartItem(CartItem cartItem) => Create(cartItem);

        public void DeleteOneCartItem(int cartItem_id, CartItem cartItem)
        {
            var entity = GetOneCartItemById(cartItem_id, false);
            if (entity == null)
                throw new CartNotFound(cartItem_id);
            Delete(entity);
        }

        public IQueryable<CartItem> GetAllCartItems(bool trackChanges) => FindAll(trackChanges)
            .Include(ci => ci.Cart)
            .Include(ci => ci.Product);

        public IQueryable<CartItem> GetCartItemsByCartId(int cart_id, bool trackChanges) => FindByCondition(ci => ci.cart_id == cart_id, trackChanges)
            .Include(ci => ci.Cart)
            .Include(ci => ci.Product);


        public CartItem GetOneCartItemById(int cartItem_id, bool trackChanges) =>
            FindByCondition(ci => ci.cartItem_id == cartItem_id, trackChanges)
            .Include(ci => ci.Cart)
            .Include(ci => ci.Product)
            .SingleOrDefault() ?? throw new CartNotFound(cartItem_id);

        public void UpdateOneCartItem(int cartItem_id, CartItem cartItem)
        {
           var entity = GetOneCartItemById(cartItem_id, true);
            if (entity == null)
                throw new CartNotFound(cartItem_id);
            entity.cart_id = cartItem.cart_id;
            entity.product_id = cartItem.product_id;
            entity.quantity = cartItem.quantity;
            entity.addedDate = cartItem.addedDate;
            Update(entity);
        }
    }
}
