using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class OrderDetails
    {
        [Key]
        public int orderDetails_id { get; set; }
        public int order_id { get; set; }
        public decimal totalAmount { get; set; }
        public bool isPaid { get; set; }
        public string? status { get; set; }

        public Order? Order { get; set; }
    }
}
