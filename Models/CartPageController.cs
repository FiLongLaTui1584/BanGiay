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
        public ActionResult CartPage()
        {
            List<CartPageController> list = new List<CartPageController>();
            return View();
        }
    }
}