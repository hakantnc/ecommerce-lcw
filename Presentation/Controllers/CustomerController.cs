using Entities.DTO;
using Entities.DTO.CustomerDTO;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IServiceManager _manager;
        private readonly ILoggerService _logger;

        public CustomerController(IServiceManager manager, ILoggerService logger)
        {
            _manager = manager;
            _logger = logger;
        }

        // GET: api/customers ALL CUSTOMERS
        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            _logger.LogInfo("Fetching all customers");
            var customers = _manager.Customer.GetAllCustomers(false);
            var response = customers.Select(c => new
            {
                id = c.Id,
                firstName = c.FirstName,
                lastName = c.LastName,
                email = c.Email,
                phoneNumber = c.PhoneNumber,
                address = c.Address,
                city = c.City,
                country = c.Country,
            });
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            _logger.LogInfo($"Fetching customer with ID: {id}");
            var customer = _manager.Customer.GetOneCustomerById(id, false);
            if (customer == null)
            {
                throw new CustomerNotFound(id);
            }
            var response = new
            {
                id = customer.Id,
                firstName = customer.FirstName,
                lastName = customer.LastName,
                email = customer.Email,
                phoneNumber = customer.PhoneNumber,
                address = customer.Address,
                city = customer.City,
                country = customer.Country,
            };
            return Ok(response);
        }

        // POST: api/customers/ CREATE CUSTOMER
        [HttpPost]
        public IActionResult CreateOneCustomer([FromBody] Customer customer)
        {
            _logger.LogInfo("Creating a new customer");
            if (customer == null)
            {
                return BadRequest("Customer data is invalid");
            }
            _manager.Customer.CreateOneCustomer(customer);
            _manager.Save();
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        // PUT: api/customers/{id} UPDATE CUSTOMER
        [HttpPut("{id:int}")]
        public IActionResult UpdateOneCustomer(int id, [FromBody] CustomerDtoForUpdate customerDto)
        {
            _logger.LogInfo($"Updating customer with ID: {id}");
            if (customerDto == null || customerDto.Id != id)
            {
                return BadRequest("Customer data is invalid");
            }
            var existingCustomer = _manager.Customer.GetOneCustomerById(id, false);
            if (existingCustomer == null)
            {
                return NotFound();
            }
            _manager.Customer.UpdateOneCustomer(id, customerDto, false);
            _manager.Save();
            return NoContent();
        }

        // DELETE: api/customers/{id} DELETE CUSTOMER
        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneCustomer(int id)
        {
            _logger.LogInfo($"Deleting customer with ID: {id}");
            var customer = _manager.Customer.GetOneCustomerById(id, false);
            if (customer == null)
            {
                return NotFound();
            }
            _manager.Customer.DeleteOneCustomer(id, customer);
            _manager.Save();
            return NoContent();
        }
    }
}
