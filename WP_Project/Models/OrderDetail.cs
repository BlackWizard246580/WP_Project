using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WP_Project.Models
{
    public class OrderDetail
    {
        [Key]
        [Column(Order = 1)]
        public int OrderID { get; set; }

        [Key]
        [Column(Order = 2)]
        public int ItemID { get; set; }
        public int Qty { get; set; }
        public double Amount { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}