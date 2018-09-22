using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WP_Project.Models;
namespace WP_Project.ViewModels
{
    public class ItemDetailVM
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public double ItemPrice { get; set; }
        public string Image { get; set; }
        public Category Category { get; set; }
        public List<CustomField> CustomFields { get; set; }
    }
}