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
        public int CategoryID { get; set; }
        public string Image { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<CustomField> CustomFields { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}