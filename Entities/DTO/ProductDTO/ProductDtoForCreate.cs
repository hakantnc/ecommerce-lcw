using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.ProductDTO
{
    public record ProductDtoForCreate
        (
           [Required(ErrorMessage = "Name is required.")]
           [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
           string Name,


           [Required(ErrorMessage = "Description is required.")]
           [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
           string Description,


           string ImageUrl,


           [Required(ErrorMessage = "Stock is required.")]
           [Range(0, int.MaxValue, ErrorMessage = "Stock must be a non-negative integer.")]
           int Stock,


           [Required(ErrorMessage = "Price is required.")]
           [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
           decimal Price,


           [Required(ErrorMessage = "Category ID is required.")]
           [Range(1, int.MaxValue, ErrorMessage = "Category ID must be a positive integer.")]
           int category_id,
           

           [Required(ErrorMessage = "Supplier ID is required.")]
           [Range(1, int.MaxValue, ErrorMessage = "Supplier ID must be a positive integer.")]
           int SupplierId,


           [Required(ErrorMessage = "IsActive is required.")]
           bool IsActive = true


        )
    {
    }
}
