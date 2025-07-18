using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface ICustomerRepository : IRepositoryBase<Customer>
    {
        IQueryable<Customer> GetAllCustomers(bool trackChanges);
        public Customer GetOneCustomerById(int customer_id, bool trackChanges);
        void CreateOneCustomer(Customer customer);
        void UpdateOneCustomer(int customer_id, Customer customer, bool trackChanges);
        void DeleteOneCustomer(int customer_id, Customer customer);
    }
}
