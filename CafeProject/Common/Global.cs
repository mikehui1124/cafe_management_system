using CafeProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeProject.Common
{
    //define a Global variable (ie. UserInfo) that conforms to UserInfo.class and is 'static'
    public static class Global
    {
        public static UserInfo UserInfo { get; set; } = new UserInfo();
    }
}
