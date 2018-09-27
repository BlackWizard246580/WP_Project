using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WP_Project.Models;

namespace WP_Project.ViewModels
{
    public class CartViewVM
    {
        public CartViewVM()
        {

        }

        public CartViewVM(Item item)
        {
            ItemID = item.ItemID;
            ItemName = item.ItemName;
            ItemPrice = item.ItemPrice;
            CategoryName = item.Category.CategoryName;
        }

        public CartViewVM(int itemID, string itemName, double itemPrice, string categoryName, int qty)
        {
            ItemID = itemID;
            ItemName = itemName;
            ItemPrice = itemPrice;
            CategoryName = categoryName;
            QTY = qty;
        }

        public int ItemID { get; set; }
        public string Key { get; set; }
        public string ItemName { get; set; }
        public double ItemPrice { get; set; }
        public string CategoryName { get; set; }
        public int QTY { get; set; }
        public List<ItemCustomFieldName> ItemCustomFieldName { get; set; }

        public double SubTotal { get; set; }
    }
}