using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WP_Project.Models;

namespace WP_Project.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        // GET: Cart/AddToCart/id
        [HttpGet]
        public ActionResult AddToCart(int id)
        {
            //No Need
            //Item Item = db.Item.Where(x => x.ItemID == id).First();

            if (Session["cartItems"] == null)
            {
                List<CartItem> cartItems = new List<CartItem>();
                cartItems.Add(new CartItem { ItemID = id, QTY = 1 });
                Session["cartItems"] = cartItems;
                Session["cartItemCount"] = cartItems.Count();
            }
            else
            {
                List<CartItem> cartItems = (List<CartItem>)Session["cartItems"];
                int index = isExist(id, cartItems);
                if (index != -1)
                {
                    cartItems[index].QTY++;
                }
                else
                {
                    cartItems.Add(new CartItem { ItemID = id, QTY = 1 });
                }
                Session["cartItems"] = cartItems;
                Session["cartItemCount"] = cartItems.Count();
            }

            //return RedirectToAction("Index", "Item");
            
            return Content((Session["cartItemCount"]).ToString());
        }

        // POST: Cart/AddToCart
        [HttpPost]
        public ActionResult AddToCart(CartItem cartItem)
        {
            return Content("ID >>> ");
               //No Need
               //Item Item = db.Item.Where(x => x.ItemID == id).First();
            //   List <CartItem> cartItems;

            //if (Session["cartItems"] == null)
            //{
            //    cartItems = new List<CartItem>();
            //    cartItems.Add(new CartItem { ItemID = id, QTY = 1 });
            //}
            //else
            //{
            //    cartItems = (List<CartItem>)Session["cartItems"];
            //    int index = isExist(id, cartItems);
            //    if (index != -1)
            //    {
            //        cartItems[index].QTY++;
            //    }
            //    else
            //    {
            //        cartItems.Add(new CartItem { ItemID = id, QTY = 1 });
            //    }
            //}
            //if(customfields != null)
            //{
            //    //foreach(CustomField => customfields)
            //    //{

            //    //}
            //}

            //Session["cartItems"] = cartItems;
            //Session["cartItemCount"] = cartItems.Count();

            ////return RedirectToAction("Index", "Item");

            //return Content((Session["cartItemCount"]).ToString());
        }

        private int isExist(int itemId, List<CartItem> cartItems)
        {
            for (int i = 0; i < cartItems.Count; i++)
                if (cartItems[i].ItemID == itemId)
                    return i;
            return -1;
        }
    }
}