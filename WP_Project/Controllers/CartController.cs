using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WP_Project.Models;
using WP_Project.ViewModels;

namespace WP_Project.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cart
        public ActionResult Index()
        {
            List<CartItem> cartItems = (List<CartItem>)Session["cartItems"];
            List<CartViewVM> cartViewVM = new List<CartViewVM>();
            foreach (CartItem item in cartItems)
            {
                List<ItemCustomField> ItemCF = item.ItemCustomFields;

                Item tmp_item = db.Item.Where(x => x.ItemID == item.ItemID)
                    .FirstOrDefault();
                CartViewVM cartVM = new CartViewVM(tmp_item);
                cartVM.QTY = item.QTY;
                
                if(item.ItemCustomFields != null) {
                    cartVM.ItemCustomFieldName = new List<ItemCustomFieldName>();
                    foreach (ItemCustomField icf in ItemCF)
                    {
                        CustomField tmp_cf = db.CustomField.Where(x => x.CustomFieldID == icf.CustomFieldID).FirstOrDefault();
                        CustomFieldValue tmp_cfv = db.CustomFieldValue.Where(x => x.CustomFieldValueID == icf.CustomFieldValueID).FirstOrDefault();
                        ItemCustomFieldName item_cfn = new ItemCustomFieldName(tmp_cf, tmp_cfv);

                        if(item_cfn != null)
                        { 
                            cartVM.ItemCustomFieldName.Add(item_cfn);
                        }
                    }
                }

                cartViewVM.Add(cartVM);
            }
            return View(cartViewVM);
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
            //return Content("ID >>> ");
            if (cartItem != null)
            {
                cartItem.QTY = (cartItem.QTY <= 0) ? 1 : cartItem.QTY;
                List<ItemCustomField> itemsCF = cartItem.ItemCustomFields.ToList();

                if (Session["cartItems"] == null)
                {
                    List<CartItem> cartItems = new List<CartItem>();
                    cartItems.Add(cartItem);
                    Session["cartItems"] = cartItems;
                    Session["cartItemCount"] = cartItems.Count();
                }
                else
                {
                    List<CartItem> cartItems = (List<CartItem>)Session["cartItems"];
                    int index = isExist(cartItem.ItemID, cartItems);
                    if (index != -1)
                    {
                        CartItem chk_cartItem = cartItems[index];
                        if(chk_cartItem.ItemCustomFields != null)
                        {
                            bool isSame = true;
                            for (int i = 0; i < itemsCF.Count; i++)
                            {
                                if ((chk_cartItem.ItemCustomFields[i].CustomFieldID == itemsCF[i].CustomFieldID)
                                    && (chk_cartItem.ItemCustomFields[i].CustomFieldValueID != itemsCF[i].CustomFieldValueID))
                                { isSame = false; break; }
                            }

                            if (isSame)
                            {
                                cartItems[index].QTY++;
                            }
                            else
                            {
                                cartItems.Add(cartItem);
                            }
                        }
                    }
                    else
                    {
                        cartItems.Add(cartItem);
                    }
                    Session["cartItems"] = cartItems;
                    Session["cartItemCount"] = cartItems.Count();
                }

                return Json((Session["cartItemCount"]).ToString());
                //return Content((Session["cartItemCount"]).ToString());
            }
            else
            {
                return Json("An Error Has occoured");
            }
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