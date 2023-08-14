using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeProject.Models
{
    public class Item : CommonProps
    {
        public int ItemId { get; set; } = 0;
        public string ItemCode { get; set; } = "";
        public string ItemName { get; set; } = "";
        public string Category { get; set; } = "";
        public decimal Price { get; set; } = 0;
    }
}
