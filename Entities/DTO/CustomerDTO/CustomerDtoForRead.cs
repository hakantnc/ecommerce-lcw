using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.CustomerDTO
{
    public record CustomerDtoForRead(
        int Id,
        string FirstName,
        string LastName,
        string Address,
        string City,
        string Country,
        string Email,
        string PhoneNumber
        // ⚠️ Password dahil edilmiyor - güvenlik!
    );
}
