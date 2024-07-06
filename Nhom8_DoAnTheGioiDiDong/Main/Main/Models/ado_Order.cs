using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Main.Models
{
    public class ado_Order
    {
        public int id { get; set; }       
        public int userId { get; set; }
        public DateTime createAt { get; set; }
        public int State { get; set; }
        public DateTime deliveryDate { get; set; }
        [Required(ErrorMessage = "Vui lòng không để trống")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Vui lòng không để trống")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Vui lòng không để trống")]
        public string Name { get; set; }
        public float SumPrice { get; set; }

    }
}