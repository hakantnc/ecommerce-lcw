using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public ICollection<PaymentMethods>? PaymentMethods { get; set; }
    }
}
