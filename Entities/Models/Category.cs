using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Category
    {
        [Key]
        public int category_id { get; set; }
        public string? category_name { get; set; }
        public string? category_description { get; set; }
        public ICollection<Product>? Products { get; set; }
        public ICollection<Subcategory> Subcategories { get; set; } = new List<Subcategory>();

    }
}
