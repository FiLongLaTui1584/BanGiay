using BanGiay.Context;
using BanGiay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanGiay.Controllers
{
    public class CartPageController : Controller
    {
        // GET: CartPage
        CNPMEntities1 _db = new CNPMEntities1();
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        // GET: ShoppingCart
        public ActionResult AddToCart(int id)
        {
            var pro = _db.SanPhams.SingleOrDefault(s => s.maSP == id);
            if (pro != null)
            {
                GetCart().Add(pro);
            }

            return RedirectToAction("CartPage", "CartPage");
        }
        public ActionResult CartPage()
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("CartPage", "CartPage");
            }
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }

        public ActionResult Update_Quantity_Cart(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int id_pro = int.Parse(form["ID_Product"]);
            int quantity = int.Parse(form["Quantity"]);
            cart.Update_Quantity_Shopping(id_pro, quantity);
            return RedirectToAction("CartPage", "CartPage");
        }

    }
}