using Entities.Exceptions;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class CartRepository : RepositoryBase<Cart>, ICartRepository
    {
        public CartRepository(AppDbContext context) : base(context)
        {
        }

        public void CreateOneCart(Cart cart) => Create(cart);

        public void DeleteOneCart(int cart_id, Cart cart)
        {
            var entity = GetOneCartById(cart_id, false);
            if (entity == null)
                throw new CartNotFound(cart_id);
            Delete(entity);
        }

        public Cart GetActiveCartByCustomerId(int customer_id, bool trackChanges) => FindByCondition(c => c.customer_id == customer_id && c.IsActive, trackChanges)
            .Include(c => c.Customer)
            .Include(c => c.CartItems)
            .FirstOrDefault() ?? throw new CartNotFound(customer_id);


        public IQueryable<Cart> GetAllCarts(bool trackChanges)=> FindAll(trackChanges)
            .Include(c => c.Customer)
            .Include(c => c.CartItems);

        public Cart GetOneCartById(int cart_id, bool trackChanges) => FindByCondition(c => c.Id == cart_id, trackChanges)
            .Include(c => c.Customer)
            .Include(c => c.CartItems)
            .FirstOrDefault() ?? throw new CartNotFound(cart_id);

        public void UpdateOneCart(int cart_id, Cart cart) { 
         
            var entity = GetOneCartById(cart_id, true);
            if (entity == null)
                throw new CartNotFound(cart_id);
            entity.customer_id = cart.customer_id;
            entity.IsActive = cart.IsActive;
            entity.CartItems = cart.CartItems;
            Update(entity);




        }
    }
}
