﻿using System;
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
        CNPM_Entities objCNPMEntities = new CNPM_Entities();

        public ActionResult Index()
        {
            var listSP = objCNPMEntities.SanPhams.ToList();
            var listCat = objCNPMEntities.Categories.ToList();
            var listTH = objCNPMEntities.ThuongHieuSPs.ToList();

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

        public ActionResult Search(string query)
        {
            var listSP = objCNPMEntities.SanPhams
                .Where(sp => sp.tenSP.Contains(query))
                .ToList();

            var viewModel = new ViewModel
            {
                SanPham = listSP
            };

            return View("SearchResults", viewModel);
        }
        public ActionResult Viewall() 
        {
            var listSP = objCNPMEntities.SanPhams.ToList();


            var viewModel = new ViewModel
            {
                SanPham = listSP,
            };

            return View(viewModel);
        }






        [HttpPost]
        public JsonResult AddToFavorites(int maSP)
        {
            if (Session["UserID"] == null)
            {
                return Json(new { success = false, message = "Bạn cần đăng nhập để thêm sản phẩm vào yêu thích." });
            }

            int userId = (int)Session["UserID"];

            var existingFavorite = objCNPMEntities.SPYeuThiches
                .FirstOrDefault(f => f.maSP == maSP && f.maUser == userId);

            if (existingFavorite == null)
            {
                var newFavorite = new SPYeuThich
                {
                    maSP = maSP,
                    maUser = userId
                };

                objCNPMEntities.SPYeuThiches.Add(newFavorite);
                objCNPMEntities.SaveChanges();
            }

            return Json(new { success = true, message = "Sản phẩm đã được thêm vào yêu thích." });
        }

    }
}
