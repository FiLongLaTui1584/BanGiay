using BanGiay.Context;
using BanGiay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanGiay.Controllers
{
    public class ProductController : Controller
    {
        CNPM_Entities objCNPMEntities = new CNPM_Entities();
        // GET: Product

        public ActionResult Detail(int maSP)
        {
            var objSP = objCNPMEntities.SanPhams.Where(n => n.maSP == maSP).FirstOrDefault();
            var objThuongHieu = objCNPMEntities.ThuongHieuSPs.Find(objSP.MaTH);
            var objCate = objCNPMEntities.Categories.Find(objSP.CatID);

            var viewModel = new ViewModel
            {
                SanPham1 = objSP,
                TenThuongHieu = objThuongHieu != null ? objThuongHieu.TenTH : string.Empty,
                Category1 = objCate
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