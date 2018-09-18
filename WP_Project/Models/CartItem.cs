using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WP_Project.Models
{
    public class CartItem
    {
        public int ItemID { get; set; }
        public int QTY { get; set; }
        public List<ItemCustomField> ItemCustomFields { get; set; }
    }
}