using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.ProductDTO
{
    public record ProductDtoForRead
    (
         int Id,
        string Name,
        string Description,
        decimal Price,
        int Stock,
        string? ImageUrl,
        bool IsActive,
        int CategoryId,
        string CategoryName,
        int SupplierId,
        string SupplierName
    )
    {
    }
}
