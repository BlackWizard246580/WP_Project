using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace WP_Project.Models
{
    public class OrderDetailCustomField
    {
        public int OrderDetailCustomFieldID { get; set; }
        public int OrderDetailID { get; set; }
        public string CustomFieldName { get; set; }
        public string CustomFieldValueName { get; set; }
        public double AdditionalPrice { get; set; }
        public virtual OrderDetail OrderDetail { get; set; }
    }
}