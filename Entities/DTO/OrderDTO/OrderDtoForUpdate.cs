﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.OrderDTO
{
    public record OrderDtoForUpdate(
        int Id,
        int CartId,
        int PaymentId
        )
    {
    }
}
