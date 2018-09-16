using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WP_Project.Models
{
    public class CustomFieldValue
    {
        public int CustomFieldValueID { get; set; }
        public string CustomFieldValueName { get; set; }
        public int CustomFieldID { get; set; }
        public double AdditionalPrice { get; set; }

        public virtual ICollection<CustomField> CustomFields { get; set; }

        public CustomField CustomField { get; set; }
    }
}