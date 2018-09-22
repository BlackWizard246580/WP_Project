using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WP_Project.Models
{
    public class OrderDetail
    {
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int ItemID { get; set; }
        public int Qty { get; set; }
        public double SubTotalAmount { get; set; }

        public virtual Order Order { get; set; }
        public virtual Item Item { get; set; }
        public virtual ICollection<OrderDetailCustomField> OrderDetailCustomFields { get; set; }
    }
}