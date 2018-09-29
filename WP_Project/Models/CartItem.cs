using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WP_Project.Models
{
    public class CartItem
    {
        public string Key { get; set; }
        public int ItemID { get; set; }
        public int Qty { get; set; }
        public List<ItemCustomField> ItemCustomFields { get; set; }
    }
}