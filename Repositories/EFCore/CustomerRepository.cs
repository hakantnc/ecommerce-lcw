using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(AppDbContext context) : base(context)
        {
        }

        public void CreateOneCustomer(Customer customer) => Create(customer);

        public void DeleteOneCustomer(int customer_id, Customer customer)
        {
            var entity = GetOneCustomerById(customer_id, false);
            if(entity == null)
                throw new CustomerNotFound(customer_id);
            Delete(entity);
        }

        public IQueryable<Customer> GetAllCustomers(bool trackChanges) => FindAll(trackChanges);

        public Customer GetOneCustomerById(int customer_id, bool trackChanges) => FindByCondition(c=> c.Id == customer_id, trackChanges)
            .SingleOrDefault() ?? throw new CustomerNotFound(customer_id);

        public void UpdateOneCustomer(int customer_id, Customer customer, bool trackChanges)
        {
            var entity = GetOneCustomerById(customer_id, false);
            if(entity == null)
                throw new CustomerNotFound(customer_id);
            entity.FirstName = customer.FirstName;
            entity.Email = customer.Email;
            entity.LastName = customer.LastName;
            entity.PhoneNumber = customer.PhoneNumber;
            entity.Address = customer.Address;
            entity.City = customer.City;
            entity.Country = customer.Country;
            entity.Password = customer.Password;
            Update(entity);

        }
    }
}
