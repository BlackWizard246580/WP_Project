using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WP_Project.Models
{
    public class ItemCustomFieldName
    {
        public ItemCustomFieldName()
        {

        }
        public ItemCustomFieldName(CustomField customField, CustomFieldValue customFieldValue)
        {
            CustomFieldID = customField.CustomFieldID;
            CustomFieldName = customField.CustomFieldName;
            CustomFieldValueID = customFieldValue.CustomFieldValueID;
            CustomFieldValueName = customFieldValue.CustomFieldValueName;
            AdditionalPrice = customFieldValue.AdditionalPrice;
        }

        public int CustomFieldID { get; set; }
        public string CustomFieldName { get; set; }
        public int CustomFieldValueID { get; set; }
        public string CustomFieldValueName { get; set; }
        public double AdditionalPrice { get; set; }
    }
}
