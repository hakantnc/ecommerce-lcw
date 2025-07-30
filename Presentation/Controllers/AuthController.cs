using Entities.DTO.AuthDTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/auth")]

    public class AuthController : ControllerBase
    {
        private readonly IRepositoryManager _manager;
        private readonly IJwtService _jwtService;

        public AuthController(IRepositoryManager manager, IJwtService jwtService)
        {
            _manager = manager;
            _jwtService = jwtService;
        }
        [HttpPost("customer/register")]
        public IActionResult RegisterCustomer([FromBody] CustomerDtoForRegister dto)
        {
            if (_manager.CustomerService.GetByEmail(dto.Email) != null)
            {
                return BadRequest("Email already exists.");
            }
            var customer = new Customer
            {
                Email = dto.Email,
                Password = PasswordHasher.Hash(dto.Password),
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };
            _manager.CustomerService.Create(customer);
            _manager.Save();
            return Ok("Customer Registered");

        }

        [HttpPost("supplier/register")]
        public IActionResult RegisterSupplier([FromBody] SupplierDtoForRegister dto)
        {
            if (_manager.SupplierService.GetByEmail(dto.Email) != null)
            {
                return BadRequest("Email already exists.");
            }
            var supplier = new Supplier
            {
                supplier_email = dto.Email,
                supplier_password = PasswordHasher.Hash(dto.Password),
                supplier_name = dto.SupplierName
            };
            _manager.SupplierService.Create(supplier);
            _manager.Save();
            return Ok("Supplier Registered");
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        { 
            var customer = _manager.CustomerService.GetByEmail(dto.Email);
            if (customer != null && PasswordHasher.Verify(dto.Password, customer.Password!))
            {
                
               var token = _jwtService.GenerateToken(customer.Email!, "Customer", customer.FirstName, customer.LastName, customer.Id);
                return Ok(new {token});
            }

              var supplier = _manager.SupplierService.GetByEmail(dto.Email);
            if( supplier != null && PasswordHasher.Verify(dto.Password, supplier.supplier_password!))
            {

                var token = _jwtService.GenerateToken(supplier.supplier_email!, "Supplier", supplier.supplier_name, supplier.supplier_name, supplier.supplier_id);
                return Ok(new {token});
            }
            return StatusCode(401, "Invalid email or password");
        }



    }
}
