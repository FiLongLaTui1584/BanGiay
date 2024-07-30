using BanGiay.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanGiay.Controllers
{
    public class FavoriteController : Controller
    {
        CNPM_Entities objCNPMEntities = new CNPM_Entities();

        public ActionResult Favorite()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            int userId = (int)Session["UserID"];
            var favoriteProducts = objCNPMEntities.SPYeuThiches
                .Where(f => f.maUser == userId)
                .Select(f => f.SanPham)
                .ToList();

            return View(favoriteProducts);
        }

        [HttpPost]
        public ActionResult RemoveFromFavorites(int maSP)
        {
            if (Session["UserID"] == null)
            {
                return Json(new { success = false, message = "Bạn cần đăng nhập để xóa sản phẩm khỏi yêu thích." });
            }

            int userId = (int)Session["UserID"];
            var favorite = objCNPMEntities.SPYeuThiches
                .FirstOrDefault(f => f.maSP == maSP && f.maUser == userId);

            if (favorite != null)
            {
                objCNPMEntities.SPYeuThiches.Remove(favorite);
                objCNPMEntities.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Sản phẩm không tồn tại trong danh sách yêu thích." });
        }
    }
}