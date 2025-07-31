using Entities.DTO.AuthDTO;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private void SetTokenCookies(string accessToken, string refreshToken)
        {
            var accessOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(15)
            };

            var refreshOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            Response.Cookies.Append("access_token", accessToken, accessOptions);
            Response.Cookies.Append("refresh_token", refreshToken, refreshOptions);
        }
        private IActionResult IssueNewTokens(string email, string role, string firstName, string lastName, int id, object user)
        {
            var claims = new[]
            {
        new Claim(ClaimTypes.Email, email),
        new Claim(ClaimTypes.Role, role),
        new Claim(ClaimTypes.GivenName, firstName),
        new Claim(ClaimTypes.Surname, lastName),
        new Claim(ClaimTypes.NameIdentifier, id.ToString())
    };

            var newAccessToken = _jwtService.GenerateAccessToken(claims);
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            if (user is Customer c)
            {
                c.RefreshToken = newRefreshToken;
                c.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            }
            else if (user is Supplier s)
            {
                s.RefreshToken = newRefreshToken;
                s.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            }

            _manager.Save();
            SetTokenCookies(newAccessToken, newRefreshToken);
            return Ok("Tokens refreshed");
        }


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
            
            var hashedPassword = PasswordHasher.Hash(dto.Password);
            
            var customer = new Customer
            {
                Email = dto.Email,
                Password = hashedPassword,
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
            
            if (customer != null)
            {
                var isPasswordValid = PasswordHasher.Verify(dto.Password, customer.Password!);
            }
            
            if (customer != null && PasswordHasher.Verify(dto.Password, customer.Password!))
            {
                var claims = new[]
                {
                new Claim(ClaimTypes.Email, customer.Email),
                new Claim(ClaimTypes.Role, "Customer"),
                new Claim(ClaimTypes.GivenName, customer.FirstName),
                new Claim(ClaimTypes.Surname, customer.LastName),
                new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString())
            };

                var accessToken = _jwtService.GenerateAccessToken(claims);
                var refreshToken = _jwtService.GenerateRefreshToken();

                customer.RefreshToken = refreshToken;
                customer.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
                _manager.Save();
                SetTokenCookies(accessToken, refreshToken);
                return Ok("Login successful");

            }

            var supplier = _manager.SupplierService.GetByEmail(dto.Email);
            if (supplier != null && PasswordHasher.Verify(dto.Password, supplier.supplier_password!))
            {
                var claims = new[]
                {
                new Claim(ClaimTypes.Email, supplier.supplier_email!),
                new Claim(ClaimTypes.Role, "Supplier"),
                new Claim(ClaimTypes.GivenName, supplier.supplier_name),
                new Claim(ClaimTypes.Surname, supplier.supplier_name),
                new Claim(ClaimTypes.NameIdentifier, supplier.supplier_id.ToString())
            };

                var accessToken = _jwtService.GenerateAccessToken(claims);
                var refreshToken = _jwtService.GenerateRefreshToken();

                supplier.RefreshToken = refreshToken;
                supplier.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
                _manager.Save();

                SetTokenCookies(accessToken, refreshToken);
                return Ok("Login successful");
            }

            return StatusCode(401, "Invalid email or password");
        }

        [HttpPost("refresh")]
        public IActionResult Refresh()
        {
            var refreshToken = Request.Cookies["refresh_token"];
            if (refreshToken == null) return StatusCode(401, "Token Hatası."); ;

            var customer = _manager.CustomerService.GetByRefreshToken(refreshToken);
            if (customer != null && customer.RefreshTokenExpiryTime > DateTime.UtcNow)
            {
                return IssueNewTokens(customer.Email!, "Customer", customer.FirstName, customer.LastName, customer.Id, customer);
            }

            var supplier = _manager.SupplierService.GetByRefreshToken(refreshToken);
            if (supplier != null && supplier.RefreshTokenExpiryTime > DateTime.UtcNow)
            {
                return IssueNewTokens(supplier.supplier_email!, "Supplier", supplier.supplier_name, supplier.supplier_name, supplier.supplier_id, supplier);
            }

            return StatusCode(401, "Token Hatası.");
        }
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("access_token");
            Response.Cookies.Delete("refresh_token");
            return Ok("Logged out");
        }
        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var email = identity?.FindFirst(ClaimTypes.Email)?.Value;
            var role = identity?.FindFirst(ClaimTypes.Role)?.Value;
            var firstName = identity?.FindFirst(ClaimTypes.GivenName)?.Value;
            var lastName = identity?.FindFirst(ClaimTypes.Surname)?.Value;

            return Ok(new
            {
                email,
                role,
                given_name = firstName,
                surname = lastName
            });
        }



    }
}
