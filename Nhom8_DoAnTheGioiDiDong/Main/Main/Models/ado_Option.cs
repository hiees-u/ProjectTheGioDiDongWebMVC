using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Main.Models
{
    public class ado_Option
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public string img { get; set; }
        public int ProductId { get; set; }
    }
}