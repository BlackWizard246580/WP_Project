using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace WP_Project.ViewModels
{
    public class OrderFormVM
    {
        [Required]
        [Display(Name = "Deliver Address")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Deliver Date")]
        public DateTime OrderDateTime { get; set; }
    }
}