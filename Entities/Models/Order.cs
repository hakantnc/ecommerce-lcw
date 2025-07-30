using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int cart_id { get; set; }
        public int payment_id { get; set; }

        public PaymentMethods? PaymentMethod { get; set; }
        public Cart? Cart { get; set; }
        public ICollection<OrderDetails>? OrderDetails { get; set; }
    }
}
