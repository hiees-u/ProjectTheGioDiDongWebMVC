using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Main.Models
{
    public class ado_UserInfo
    {
        public int userId { get; set; }
        public string accountName { get; set; }
        public string Address { get; set; }
        public int Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}