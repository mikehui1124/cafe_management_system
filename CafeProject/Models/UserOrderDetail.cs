using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeProject.Models
{
    public class UserOrderDetail 
    {
        public int UserOrderDetailId { get; set; } = 0;
        public int UserOrderId { get; set; } = 0;
        public int ItemId { get; set; } = 0;
        public decimal UnitPrice { get; set; } = 0;
        public int Qty { get; set; } = 0;

        //View Properties
        public string ItemCode { get; set; } = "";
        public string ItemName { get; set; } = "";
        public string Category { get; set; } = "";
    }
}
