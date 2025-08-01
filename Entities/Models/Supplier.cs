using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Supplier
    {
        public int supplier_id { get; set; }
        public string? supplier_password { get; set; }
        public string supplier_name { get; set; } = string.Empty;
        public string supplier_email { get; set; } = string.Empty;
        public string? supplier_phone { get; set; }
        public string supplier_address { get; set; } = string.Empty;
        public string supplier_city { get; set; } = string.Empty;
        public string supplier_country { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public ICollection<Product>? products { get; set; }
    }
}