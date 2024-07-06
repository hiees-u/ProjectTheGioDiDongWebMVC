using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Main.Models
{
    public class ado_Product_Option
    {
        public int id { get; set; }
        public string name { get; set; }
        public string decription { get; set; }
        public int categoryId { get; set; }
        public int bardId { get; set; }
        public List<ado_Option> Options { get; set; }
    }
}