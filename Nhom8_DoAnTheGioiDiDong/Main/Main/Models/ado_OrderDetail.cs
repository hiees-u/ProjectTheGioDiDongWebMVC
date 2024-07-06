using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Main.Models
{
    public class ado_OrderDetail
    {
        public int orderId { get; set; }
        public int productId { get; set; }
        public int Capactity { get; set; }
        public string nameProduct { get; set; }
        public string nameOption { get; set; }
        public decimal price { get; set; }
        public string img { get; set; }
    }
}