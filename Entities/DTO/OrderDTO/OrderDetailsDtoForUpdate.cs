using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.OrderDTO
{
     public record OrderDetailsDtoForUpdate
    {
        public int orderDetailsId { get; set; }
        public int OrderId { get; set; }
        public decimal totalAmount { get; set; }
        public bool isPaid { get; set; }
        public string? status { get; set; }
    }
}
