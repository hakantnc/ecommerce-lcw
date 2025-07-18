using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.PaymentMethodsDTO
{
    public record PaymentMethodDtoForCreate
    (
        [Required(ErrorMessage = "Method type is required.")]
        [MaxLength(50, ErrorMessage = "Method type cannot exceed 50 characters.")]
         string MethodType,
    
         [Required(ErrorMessage = "Card holder is required.")]
         [MaxLength(100, ErrorMessage = "Card holder cannot exceed 100 characters.")]
         string CardHolder,
    
         [Required(ErrorMessage = "Card number is required.")]
         [MaxLength(20, ErrorMessage = "Card number cannot exceed 20 characters.")]
         string CardNumber,
    
         [Required(ErrorMessage = "CVV is required.")]
         [MaxLength(4, ErrorMessage = "CVV cannot exceed 4 characters.")]
         string CVV,
    
         [Required(ErrorMessage = "Expiry date is required.")]
         DateTime ExpiryDate,
    
         [Required(ErrorMessage = "Billing address is required.")]
         [MaxLength(200, ErrorMessage = "Billing address cannot exceed 200 characters.")]
         string BillingAddress,
    
         [Required(ErrorMessage = "Customer ID is required.")]
         int CustomerId



    )
    {
    }
}
