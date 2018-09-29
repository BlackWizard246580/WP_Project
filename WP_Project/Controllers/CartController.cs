using Microsoft.AspNet.Identity;
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
            if (cartItems != null)
            {
                foreach (CartItem item in cartItems)
                {
                    List<ItemCustomField> ItemCF = item.ItemCustomFields;

                    Item tmp_item = db.Item.Where(x => x.ItemID == item.ItemID)
                        .FirstOrDefault();
                    CartViewVM cartVM = new CartViewVM(tmp_item);
                    cartVM.Key = item.Key;
                    cartVM.QTY = item.Qty;

                    if (item.ItemCustomFields != null)
                    {
                        cartVM.ItemCustomFieldName = new List<ItemCustomFieldName>();
                        foreach (ItemCustomField icf in ItemCF)
                        {
                            CustomField tmp_cf = db.CustomField.Where(x => x.CustomFieldID == icf.CustomFieldID).FirstOrDefault();
                            CustomFieldValue tmp_cfv = db.CustomFieldValue.Where(x => x.CustomFieldValueID == icf.CustomFieldValueID).FirstOrDefault();
                            ItemCustomFieldName item_cfn = new ItemCustomFieldName(tmp_cf, tmp_cfv);

                            if (item_cfn != null)
                            {
                                cartVM.ItemCustomFieldName.Add(item_cfn);
                            }
                        }
                    }

                    cartViewVM.Add(cartVM);
                }
            }
            return View(cartViewVM);
        }

        // GET: Cart
        public ActionResult CheckAmount()
        {
            List<CartItem> cartItems = (List<CartItem>)Session["cartItems"];
            List<CartViewVM> cartViewVM = new List<CartViewVM>();
            double Total = 0;
            if (cartItems != null)
            {
                foreach (CartItem item in cartItems)
                {
                    List<ItemCustomField> ItemCF = item.ItemCustomFields;

                    Item tmp_item = db.Item.Where(x => x.ItemID == item.ItemID)
                        .FirstOrDefault();
                    CartViewVM cartVM = new CartViewVM(tmp_item);
                    cartVM.Key = item.Key;
                    cartVM.QTY = item.Qty;
                    cartVM.SubTotal = cartVM.QTY * cartVM.ItemPrice;

                    if (item.ItemCustomFields != null)
                    {
                        double addPrice = 0;
                        cartVM.ItemCustomFieldName = new List<ItemCustomFieldName>();
                        foreach (ItemCustomField icf in ItemCF)
                        {
                            CustomField tmp_cf = db.CustomField.Where(x => x.CustomFieldID == icf.CustomFieldID).FirstOrDefault();
                            CustomFieldValue tmp_cfv = db.CustomFieldValue.Where(x => x.CustomFieldValueID == icf.CustomFieldValueID).FirstOrDefault();
                            ItemCustomFieldName item_cfn = new ItemCustomFieldName(tmp_cf, tmp_cfv);

                            if (item_cfn != null)
                            {
                                cartVM.ItemCustomFieldName.Add(item_cfn);
                                if (item_cfn.AdditionalPrice > 0) addPrice += item_cfn.AdditionalPrice;
                            }
                        }
                        cartVM.SubTotal += (cartVM.QTY * addPrice);
                    }
                    Total += cartVM.SubTotal;
                    cartViewVM.Add(cartVM);
                }
            }
            ViewBag.Total = Total;
            return View(cartViewVM);
        }

        // GET: Cart
        [Authorize]
        public ActionResult Order()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Checkout(OrderFormVM model)
        {
            string userID = User.Identity.GetUserId();
            List<CartItem> cartItems = (List<CartItem>)Session["cartItems"];
            List<CartViewVM> cartViewVM = new List<CartViewVM>();
            double Total = 0;
            if (cartItems != null)
            {
                Order order = new Order();
                order.OrderCreateDateTime = DateTime.Now;
                order.DeliverAddress = model.Address;
                order.OrderDateTime = model.OrderDateTime;
                //order.OrderDateTime = (DateTime)Session["OrderDateTime"];
                order.UserID = userID;
                db.Order.Add(order);
                //db.SaveChanges();

                foreach (CartItem item in cartItems)
                {
                    List<ItemCustomField> ItemCF = item.ItemCustomFields;

                    Item tmp_item = db.Item.Where(x => x.ItemID == item.ItemID)
                        .FirstOrDefault();
                    CartViewVM cartVM = new CartViewVM(tmp_item);
                    cartVM.Key = item.Key;
                    cartVM.QTY = item.Qty;
                    cartVM.SubTotal = cartVM.QTY * cartVM.ItemPrice;

                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.Order = order;
                    orderDetail.ItemID = item.ItemID;
                    orderDetail.Qty = item.Qty;
                    db.OrderDetail.Add(orderDetail);

                    if (item.ItemCustomFields != null)
                    {
                        double addPrice = 0;
                        cartVM.ItemCustomFieldName = new List<ItemCustomFieldName>();
                        foreach (ItemCustomField icf in ItemCF)
                        {
                            CustomField tmp_cf = db.CustomField.Where(x => x.CustomFieldID == icf.CustomFieldID).FirstOrDefault();
                            CustomFieldValue tmp_cfv = db.CustomFieldValue.Where(x => x.CustomFieldValueID == icf.CustomFieldValueID).FirstOrDefault();
                            ItemCustomFieldName item_cfn = new ItemCustomFieldName(tmp_cf, tmp_cfv);

                            OrderDetailCustomField odcf = new OrderDetailCustomField();
                            odcf.OrderDetail = orderDetail;
                            odcf.CustomFieldName = tmp_cf.CustomFieldName;
                            odcf.CustomFieldValueName = tmp_cfv.CustomFieldValueName;
                            odcf.AdditionalPrice = tmp_cfv.AdditionalPrice;
                            db.OrderDetailCustomField.Add(odcf);

                            if (item_cfn != null)
                            {
                                cartVM.ItemCustomFieldName.Add(item_cfn);
                                if (item_cfn.AdditionalPrice > 0) addPrice += item_cfn.AdditionalPrice;
                            }
                        }
                        cartVM.SubTotal += (cartVM.QTY * addPrice);
                    }

                    orderDetail.SubTotalAmount = cartVM.SubTotal;

                    Total += cartVM.SubTotal;
                    order.Total = Total;
                    cartViewVM.Add(cartVM);
                }
                Session["cartItems"] = null;
                //Session.Remove("cartItems");
                db.SaveChanges();
            }
            ViewBag.Total = Total;
            ViewBag.CheckoutStatus = "Checkout Success!";
            return View();
        }

        [HttpPost]
        public ActionResult ChangeQty(string Key, int qty)
        {
            List<CartItem> cartItems = (List<CartItem>)Session["cartItems"];
            int index = isKeyExist(Key, cartItems);
            cartItems[index].Qty = qty;
            return Json("success");
        }

        [HttpPost]
        public ActionResult Remove(string Key)
        {
            List<CartItem> cartItems = (List<CartItem>)Session["cartItems"];
            var item = cartItems.Where(x => x.Key == Key).FirstOrDefault();
            if (item != null) cartItems.Remove(item);
            return Json("Success Removing Item from cart");
        }

        // GET: Cart/AddToCart/id
        [HttpGet]
        public ActionResult AddToCart(int id, int qty)
        {
            if (Session["cartItems"] == null)
            {
                List<CartItem> cartItems = new List<CartItem>();
                cartItems.Add(new CartItem { Key = GenerateKey(), ItemID = id, Qty = qty });
                Session["cartItems"] = cartItems;
                Session["cartItemCount"] = cartItems.Count();
            }
            else
            {
                List<CartItem> cartItems = (List<CartItem>)Session["cartItems"];
                List<int> indexList = isExist(id, cartItems);
                if (indexList == null || indexList.Count == 0)
                {
                    cartItems.Add(new CartItem { Key = GenerateKey(), ItemID = id, Qty = qty });
                }
                else
                {
                    bool statusNew = true;
                    for (int i = 0; i < indexList.Count; i++)
                    {
                        int index = indexList[i];
                        if (cartItems[index].ItemCustomFields == null)
                        {
                            statusNew = false;
                            cartItems[index].Qty += qty;
                        }
                    }
                    if (statusNew) cartItems.Add(new CartItem { Key = GenerateKey(), ItemID = id, Qty = qty });
                }
                Session["cartItems"] = cartItems;
                Session["cartItemCount"] = cartItems.Count();
            }

            return Content((Session["cartItemCount"]).ToString());
        }

        // POST: Cart/AddToCart
        [HttpPost]
        public ActionResult AddToCart(CartItem cartItem)
        {
            //return Content("ID >>> ");
            if (cartItem != null)
            {
                cartItem.Qty = (cartItem.Qty <= 0) ? 1 : cartItem.Qty;
                cartItem.Key = GenerateKey();
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
                    List<int> indexList = isExist(cartItem.ItemID, cartItems);

                    if (indexList == null || indexList.Count == 0)
                    {
                        cartItems.Add(cartItem);
                    }
                    else
                    {
                        bool found = false;
                        for (int i = 0; i < indexList.Count; i++)
                        {
                            int index = indexList[i];
                            if (cartItems[index].ItemCustomFields != null)
                            {
                                bool isSame = true;
                                for (int j = 0; j < itemsCF.Count; j++)
                                {
                                    if ((cartItems[index].ItemCustomFields[j].CustomFieldID == itemsCF[j].CustomFieldID)
                                        && (cartItems[index].ItemCustomFields[j].CustomFieldValueID != itemsCF[j].CustomFieldValueID))
                                    { isSame = false; break; }
                                }

                                if (isSame)
                                {
                                    cartItems[index].Qty += cartItem.Qty;
                                    found = true;
                                    break;
                                }
                            }
                        }

                        if (!found)
                        {
                            cartItems.Add(cartItem);
                        }
                    }
                    Session["cartItems"] = cartItems;
                    Session["cartItemCount"] = cartItems.Count();
                }

                return Json((Session["cartItemCount"]).ToString());
            }
            else
            {
                return Json("An Error Has occoured");
            }
        }

        public string GenerateKey()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }

        private List<int> isExist(int itemId, List<CartItem> cartItems)
        {
            List<int> indexList = new List<int>();
            int j = 0;
            for (int i = 0; i < cartItems.Count; i++)
                if (cartItems[i].ItemID == itemId)
                {
                    indexList.Add(i);
                }
            return indexList;
        }

        private int isKeyExist(string Key, List<CartItem> cartItems)
        {
            for (int i = 0; i < cartItems.Count; i++)
                if (cartItems[i].Key == Key)
                    return i;
            return -1;
        }
    }
}