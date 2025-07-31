using Entities.DTO;
using Entities.DTO.CustomerDTO;
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
    public class CustomerManager : ICustomerService

    {
        private readonly IRepositoryManager _manager;

        public CustomerManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public Customer CreateOneCustomer(Customer customer)
        {
            _manager.CustomerService.CreateOneCustomer(customer);
            _manager.Save();
            return customer;
        }

        public void DeleteOneCustomer(int customer_id, Customer customer)
        {
            var entity = _manager.CustomerService.GetOneCustomerById(customer_id, false);
            if (entity == null)
                throw new CustomerNotFound(customer_id);
            _manager.CustomerService.DeleteOneCustomer(customer_id, customer);
            _manager.Save();
        }

        public IEnumerable<Customer> GetAllCustomers(bool trackChanges)
        {
            return _manager.CustomerService.GetAllCustomers(trackChanges).ToList();
        }

        public Customer GetOneCustomerById(int customer_id, bool trackChanges)
        {
            return _manager.CustomerService.GetOneCustomerById(customer_id, trackChanges)
                ?? throw new CustomerNotFound(customer_id);
        }

        public void UpdateOneCustomer(int customer_id, CustomerDtoForUpdate customerDto, bool trackChanges)
        {
            var entity = _manager.CustomerService.GetOneCustomerById(customer_id, trackChanges);
            if (entity == null)
                throw new CustomerNotFound(customer_id);
            entity.FirstName = customerDto.FirstName;
            entity.LastName = customerDto.LastName;
            entity.Email = customerDto.Email;
            entity.PhoneNumber = customerDto.PhoneNumber;
            entity.Address = customerDto.Address;
            entity.City = customerDto.City;
            entity.Country = customerDto.Country;
            entity.Password = customerDto.Password;
            _manager.CustomerService.UpdateOneCustomer(customer_id, entity, trackChanges);
            _manager.Save();
        }
        public Customer? GetByRefreshToken(string refreshToken)
        {
            return _manager.CustomerService.GetByRefreshToken(refreshToken);
        }


    }

    }
