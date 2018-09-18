using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WP_Project.Models;
using WP_Project.ViewModels;

namespace WP_Project.Controllers
{
    public class ItemController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Item
        public ActionResult Index()
        {
            //retrieve Item list with Category
            var item = db.Item.Include(i => i.Category);
            return View(item.ToList());
        }

        // GET: Item/Details/{id}
        public ActionResult Details(int? id)
        {
            List<CustomField> CustomFields;
            ItemDetailVM ItemDetailVM = new ItemDetailVM();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //retrieve Item by parameter id
            Item item = db.Item.Find(id);

            if (item == null)
            {
                return HttpNotFound();
            } else
            {
                //retrieve CustomField List with CustomFieldValues by ItemID
                CustomFields = db.CustomField
                    .Where(x => x.Items.Any(y => y.ItemID == item.ItemID))
                    .ToList();

                //to carry Item with CustomField List
                ItemDetailVM.ItemID = item.ItemID;
                ItemDetailVM.ItemName = item.ItemName;
                ItemDetailVM.ItemPrice = item.ItemPrice;
                ItemDetailVM.Category = item.Category;
                ItemDetailVM.CustomFields = CustomFields;
            }
           
            return View(ItemDetailVM);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
