using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WP_Project.Controllers
{
    public class LayoutTestController : Controller
    {
        // GET: LayoutTest
        public ActionResult Index()
        {
            return View();
        }

        // GET: LayoutTest
        public ActionResult Shop()
        {
            return View();
        }

        // GET: LayoutTest
        public ActionResult ProductDetails()
        {
            return View();
        }

        // GET: LayoutTest
        public ActionResult Cart()
        {
            return View();
        }

        // GET: LayoutTest
        public ActionResult Checkout()
        {
            return View();
        }
    }
}