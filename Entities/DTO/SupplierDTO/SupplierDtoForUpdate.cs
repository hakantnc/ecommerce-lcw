using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.SupplierDTO
{
    public record SupplierDtoForUpdate
    {
        public int supplier_id { get; set; }
        public string supplier_name { get; set; } = string.Empty;
        public string supplier_email { get; set; } = string.Empty;
        public string? supplier_phone { get; set; }
        public string supplier_address { get; set; } = string.Empty;
        public string supplier_city { get; set; } = string.Empty;
        public string supplier_country { get; set; } = string.Empty;
    }
}
