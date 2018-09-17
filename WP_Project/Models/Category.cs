using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WP_Project.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}