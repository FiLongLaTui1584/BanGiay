using BanGiay.Context;
using BanGiay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanGiay.Controllers
{
    public class CategoryController : Controller
    {
        CNPMEntities objCNPMEntities = new CNPMEntities();
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductCategory(int MaTH)
        {
            var listSP = objCNPMEntities.SanPhams.Where(n => n.MaTH == MaTH).ToList();
            var listTH = objCNPMEntities.ThuongHieuSPs.Where(n => n.MaTH == MaTH).ToList();


            var viewModel = new ViewModel
            {
                SanPham = listSP,
                ThuongHieuSP = listTH
            };

            return View(viewModel);
        }
    }
}