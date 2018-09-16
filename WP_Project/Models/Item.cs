using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WP_Project.Models
{
    public class Item
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public double ItemPrice { get; set; }

        public Category Category { get; set; }
    }
}