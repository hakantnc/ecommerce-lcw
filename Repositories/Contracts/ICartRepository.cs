using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface ICartRepository : IRepositoryBase<Cart>
    {
        IQueryable<Cart> GetAllCarts(bool trackChanges);
        Cart GetOneCartById(int cart_id, bool trackChanges);
        Cart GetActiveCartByCustomerId(int customer_id, bool trackChanges);
        void CreateOneCart(Cart cart);
        void UpdateOneCart(int cart_id, Cart cart);
        void DeleteOneCart(int cart_id, Cart cart);
    }
}
