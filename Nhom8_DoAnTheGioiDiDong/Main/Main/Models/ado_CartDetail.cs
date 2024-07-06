using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Main.Models
{
    public class ado_CartDetail
    {
        public int CartId { get; set; }
        public int Capacity { get; set; }

        // option
        public int OptionId { get; set; }
        public string OpName { get; set; }
        public decimal price { get; set; }
        public string img { get; set; }

        //product
        public int ProductId { get; set; }
        public string nameProduct { get; set; }
        public string decription { get; set; }
        public int categoryId { get; set; }
        public int bardId { get; set; }

    }
}