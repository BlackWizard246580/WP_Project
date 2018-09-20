using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WP_Project.Models;

namespace WP_Project.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Category
        public ActionResult Index()
        {
            return View(db.Category.ToList());
        }

        // GET: Category/Details/5
        public ActionResult GetItems(int? id)
        {
            List<Item> itemList;
            using (db)
            {
                if (id != null)
                {
                    Category cat = db.Category.Find(id);
                    ViewBag.CategoryName = cat.CategoryName;
                    itemList = db.Item.ToArray().Where(x => x.CategoryID == cat.CategoryID).ToList();
                }
                else
                {
                    ViewBag.CategoryName = "All Items";
                    itemList = db.Item.Include(x => x.Category).ToList();
                }
            }
            return View(itemList);
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
