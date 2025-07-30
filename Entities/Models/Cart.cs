using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public int? customer_id { get; set; }
        public bool IsActive { get; set; } = true;


        public Customer? Customer { get; set; }

        public ICollection<CartItem>? CartItems { get; set; }

    }
}
