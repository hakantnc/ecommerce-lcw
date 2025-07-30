using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class CartItem
    {
        [Key]
        public int cartItem_id { get; set; }
        public int cart_id { get; set; }
        public int product_id { get; set; }
        public int quantity { get; set; }
        public DateTime addedDate { get; set; } = DateTime.Now;
        
        // Computed property - not mapped to database
        public decimal Price => Product?.Price ?? 0m;

        [JsonIgnore]
        public Cart? Cart { get; set; } 
        public Product? Product { get; set; }


    }
}
