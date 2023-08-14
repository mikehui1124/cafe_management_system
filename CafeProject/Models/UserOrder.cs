using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeProject.Models
{
    public class UserOrder : CommonProps
    {
        public int UserOrderId { get; set; } = 0;
        public string OrderNo { get; set; } = "";
        public string SellerName { get; set; } = "";

        //View Properties
        public string OrderDate { get; set; } =  "";
        public string OrderTime { get; set; } = "";
        public string OrderBy { get; set; } = "";
        public string Contact { get; set; } = "";
    }
}
