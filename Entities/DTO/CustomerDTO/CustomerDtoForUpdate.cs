using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.CustomerDTO
{
    public record CustomerDtoForUpdate(
        int Id,
        string FirstName,
        string LastName,
        string Address,
        string City,
        string Country,
        string Password,
        string Email,
        string PhoneNumber
        )
    {
    }
}
