using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeProject.Models
{
    public class UserInfo : CommonProps

    {
        public int UserId { get; set; } = 0;
        public string Username { get; set; } = "";

        public string Contact { get; set; } = "";

        public string UserPassword { get; set; } = "";
    }
}
