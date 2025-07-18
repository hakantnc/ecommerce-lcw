using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class PaymentMethods
    {
        public int Id { get; set; }
        public string? method_type { get; set; }
        public string? cardHolder { get; set; }
        public string? cardNumber { get; set; }
        public string? CVV { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string? BillingAddress { get; set; }

        public int customer_id { get; set; }

        public Customer? Customer { get; set; }

    }
}
