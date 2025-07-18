using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.CustomerDTO
{
    public record CustomerDtoForList(
        int Id,
        string FirstName,
        string LastName,
        string Email,
        string City,
        string Country
    );
}
