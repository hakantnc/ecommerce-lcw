using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DTO.PaymentMethodsDTO;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentMethodsController : ControllerBase
    {
        private readonly IServiceManager _manager;
        private readonly ILoggerService _logger;

        public PaymentMethodsController(IServiceManager manager,ILoggerService logger)
        {
            _manager = manager;
            _logger = logger;
        }
        // GET: api/paymentmethods
        [HttpGet]
        public IActionResult GetAllPaymentMethods()
        {
            _logger.LogInfo("Fetching all payment methods");
            var paymentMethods = _manager.Payment.GetAllPaymentMethods(false);
            var response = paymentMethods.Select(pm => new
            {
                id = pm.Id,
                methodType = pm.method_type,
                CardHolderName = pm.cardHolder,
                CardNumber = pm.cardNumber,
                CVV = pm.CVV,
                ExpiryDate = pm.ExpiryDate.ToString("dd-MM-yyyy"),
                BillingAddress = pm.BillingAddress,
            });
            return Ok(response);
        }
        // GET: api/paymentmethods/{id}
        [HttpGet("{id}")]
        public IActionResult GetPaymentMethods(int id) { 
            _logger.LogInfo($"Fetching payment method with ID: {id}");
            var paymentMethod = _manager.Payment.GetOnePaymentMethodById(id, false);
            if (paymentMethod == null)
            {
                throw new PaymentNotFound(id);
            }
            var response = new
            {
                id = paymentMethod.Id,
                methodType = paymentMethod.method_type,
                CardHolderName = paymentMethod.cardHolder,
                CardNumber = paymentMethod.cardNumber,
                CVV = paymentMethod.CVV,
                ExpiryDate = paymentMethod.ExpiryDate.ToString("dd-MM-yyyy"),
                BillingAddress = paymentMethod.BillingAddress,
            };
            return Ok(response);
        }
        // POST: api/paymentmethods
        [HttpPost]
        public IActionResult CreatePaymentMethod([FromBody] PaymentMethods paymentMethod)
        {
            if (paymentMethod == null)
            {
                _logger.LogError("Payment method object sent from client is null.");
                return BadRequest("Payment method object is null");
            }
            _manager.Payment.CreatePaymentMethod(paymentMethod);
            _logger.LogInfo("Payment method created successfully");
            return CreatedAtAction(nameof(GetPaymentMethods), new { id = paymentMethod.Id }, paymentMethod);
        }
        // PUT: api/paymentmethods/{id}
        [HttpPut("{id}")]
        public IActionResult UpdatePaymentMethod(int id, [FromBody] PaymentMethodDtoForUpdate paymentDto)
        {
            if (paymentDto == null)
            {
                _logger.LogError("Payment method object sent from client is null.");
                return BadRequest("Payment method object is null");
            }
            _logger.LogInfo($"Updating payment method with ID: {id}");
            _manager.Payment.UpdatePaymentMethod(id, paymentDto, false);
            _logger.LogInfo("Payment method updated successfully");
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeletePaymentMethod(int id)
        {
            _logger.LogInfo($"Deleting payment method with ID: {id}");
            var paymentMethod = _manager.Payment.GetOnePaymentMethodById(id, false);
            if (paymentMethod == null)
            {
                throw new PaymentNotFound(id);
            }
            _manager.Payment.DeletePaymentMethod(id, paymentMethod);
            _logger.LogInfo("Payment method deleted successfully");
            return NoContent();
        }
    }
}
