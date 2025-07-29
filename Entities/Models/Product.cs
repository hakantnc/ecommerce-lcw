using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; }

        public int category_id { get; set; } // Foreign key to Category - REQUIRED
        public int supplier_id { get; set; } // Foreign key to Supplier
        public int? SubcategoryId { get; set; }
        public string? SubcategoryName { get; set; } // Nullable for subcategory name
        
        public Category? Category { get; set; } // Navigation property
        public Supplier? Supplier { get; set; } // Navigation property
        public Subcategory? Subcategory { get; set; } // Navigation property
        public string? supplier_name { get; set; }
        public string? category_name { get; set; }
    }
}
