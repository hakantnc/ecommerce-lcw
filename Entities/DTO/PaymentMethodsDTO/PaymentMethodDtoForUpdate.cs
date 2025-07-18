using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.PaymentMethodsDTO
{
    public record PaymentMethodDtoForUpdate
        (
            int Id,
            string? MethodType,
            string? CardHolder,
            string? CardNumber,
            string? CVV,
            DateTime ExpiryDate,
            string? BillingAddress,
            int CustomerId
        )
    {
    }
}
