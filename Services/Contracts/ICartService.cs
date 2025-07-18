using Entities.DTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ICartService
    {
       IEnumerable<Cart> GetAllCarts(bool trackChanges);
       Cart GetOneCartById(int cart_id, bool trackChanges);
       Cart GetActiveCartByCustomerId(int customer_id, bool trackChanges);
       Cart CreateOneCart(Cart cart);
       void UpdateOneCart(int cart_id, CartDtoForUpdate cartDto);
       void DeleteOneCart(int cart_id, Cart cart);

    }
}
