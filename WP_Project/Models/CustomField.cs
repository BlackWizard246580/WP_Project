using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WP_Project.Models
{
    public class CustomField
    {
        public int CustomFieldID { get; set; }
        public string CustomFieldName { get; set; }

        public virtual ICollection<CustomFieldValue> CustomFieldValues { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}