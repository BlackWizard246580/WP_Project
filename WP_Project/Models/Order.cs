using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WP_Project.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public DateTime OrderDateTime { get; set; }
        public DateTime OrderCreateDateTime { get; set; }
        public string DeliverAddress { get; set; }
        public double Total { get; set; }
    }
}