using Entities.DTO;
using Entities.DTO.CustomerDTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAllCustomers(bool trackChanges);
        Customer GetOneCustomerById(int customer_id, bool trackChanges);
        Customer CreateOneCustomer(Customer customer);
        void UpdateOneCustomer(int customer_id, CustomerDtoForUpdate customerDto, bool trackChanges);
        void DeleteOneCustomer(int customer_id, Customer customer);
        Customer? GetByRefreshToken(string refreshToken);
    }
}
