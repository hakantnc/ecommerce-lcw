using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.ProductDTO
{
    public record ProductDtoForUpdate(
        int Id,
        int Stock,
        decimal Price,
        string Name,
        string Description,
        string ImageUrl,
        bool IsActive,
        int category_id,
        int SupplierId
    );
}

