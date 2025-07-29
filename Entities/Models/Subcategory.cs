using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Subcategory
    {
        public int SubcategoryId { get; set; }
        public string? SubcategoryName { get; set; } = string.Empty;

        public int category_id { get; set; }
        public Category? Category { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
