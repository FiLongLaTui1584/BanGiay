using BanGiay.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanGiay.Controllers
{
    public class ProductController : Controller
    {
        CNPMEntities objCNPMEntities = new CNPMEntities();
        // GET: Product
        public ActionResult Detail(int maSP)
        {
            var objProduct = objCNPMEntities.SanPhams.Where(n => n.maSP == maSP).FirstOrDefault();
            return View(objProduct);
        }
    }
}