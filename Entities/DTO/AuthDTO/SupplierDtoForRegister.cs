using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.AuthDTO
{
    public record SupplierDtoForRegister
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string SupplierName { get; set; } = null!;
    }
}
