using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public record CartDtoForUpdate(
        int Id,
        int CustomerId,
        bool IsActive = true
        )
    {

    }
}
