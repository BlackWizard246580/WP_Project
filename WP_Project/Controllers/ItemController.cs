using PagedList;
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
            var item = db.Item.Include(i => i.Category);
            return View(item.ToList());
        }

        // GET: Item
        public ViewResult PagerTest(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.ItemNameSortParm = String.IsNullOrEmpty(sortOrder) ? "Item_desc" : "";
            ViewBag.ItemPriceSortParm = sortOrder == "ItemPrice" ? "ItemPrice_desc" : "ItemPrice";
            ViewBag.CategoryNameSortParm = sortOrder == "Category" ? "Category_desc" : "Category";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var items = db.Item.Include(i => i.Category);
            //var students = from s in db.Students
            //               select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.ItemName.Contains(searchString)
                                       || s.Category.CategoryName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Item_desc":
                    items = items.OrderByDescending(s => s.ItemName);
                    break;
                case "ItemPrice":
                    items = items.OrderBy(s => s.ItemPrice);
                    break;
                case "ItemPrice_desc":
                    items = items.OrderByDescending(s => s.ItemPrice);
                    break;
                case "Category":
                    items = items.OrderBy(s => s.Category.CategoryName);
                    break;
                case "Category_desc":
                    items = items.OrderByDescending(s => s.Category.CategoryName);
                    break;
                default:  // Name ascending 
                    items = items.OrderBy(s => s.ItemName);
                    break;
            }
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(items.ToPagedList(pageNumber, pageSize));
        }

        // GET: Item/Details/5
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
            }
            else
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
                ItemDetailVM.Image = item.Image;
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
