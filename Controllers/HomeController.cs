using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanGiay.Context;
using BanGiay.Models;


namespace BanGiay.Controllers
{
    public class HomeController : Controller
    {
        CNPMEntities objCNPMEntities = new CNPMEntities();  
        public ActionResult Index()
        {
            var listSP = objCNPMEntities.SanPhams.ToList();
            var listCat = objCNPMEntities.Categories.ToList();
            var listTH  = objCNPMEntities.ThuongHieuSPs.ToList();
            

            var viewModel = new ViewModel
            {
                SanPham = listSP,
                Category = listCat,
                ThuongHieuSP = listTH
            };


            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}